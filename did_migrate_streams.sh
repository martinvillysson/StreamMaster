#!/bin/bash

# Check if $PGDATA directory exists, if not set a default value
if [ ! -d "/config/DB" ]; then
    echo "/config/DB directory does not exist. Assuming test"
    . "/var/lib/postgresql/data/env.sh"
    PGDATA=/var/lib/postgresql/data
else
    . /env.sh
fi

# Variables
batchSize=5000 # Increased batch size
dbDir="$PGDATA"
tempFileStreams="$dbDir/streams.csv"
tempFileM3UFiles="$dbDir/m3ufiles.csv"
tempFileBatch="$dbDir/batch_update.csv"
errorFile="$dbDir/did_errors.log"
tempTable="temp_batch_update"
delimiter='^' # Custom delimiter

# Clean up any existing files
rm -f "$errorFile" "$tempFileStreams" "$tempFileM3UFiles" "$tempFileBatch"

# Ensure PostgreSQL connection details are set
if [[ -z "$POSTGRES_USER" || -z "$POSTGRES_PASSWORD" || -z "$POSTGRES_DB" || -z "$POSTGRES_HOST" ]]; then
    echo "Missing required PostgreSQL environment variables."
    exit 1
fi

# Database connection command
PG_CMD="psql -h $POSTGRES_HOST -U $POSTGRES_USER -d $POSTGRES_DB"

# Query to check if the record exists
checkMigration=$($PG_CMD -t -c "SELECT COUNT(*) FROM \"SystemKeyValues\" WHERE \"Key\" = 'didIDMigration';")
checkMigration=$(echo "$checkMigration" | xargs) # Trim whitespace

if [[ "$checkMigration" -eq 0 ]]; then
    echo "didIDMigration does not exist. Proceeding with migration..."
else
    exit 0
fi

# Function to generate MD5 hash
generate_md5() {
    local key=$1
    local M3UFileId=$2
    echo -n "${key}_${M3UFileId}" | md5sum | awk '{print $1}'
}

# Function to generate M3UKey value
generate_m3u_key_value() {
    local M3UKey=$1
    local M3UFileId=$2
    local Url=$3
    local CUID=$4
    local ChannelId=$5
    local EPGID=$6
    local TVGName=$7
    local Name=$8

    local key

    case $M3UKey in
    0) key=$Url ;;
    1) key=$CUID ;;
    2) key=$ChannelId ;;
    3) key=$EPGID ;;
    4) key=${TVGName:-$Name} ;;
    5)
        if [[ -n $TVGName && -n $EPGID ]]; then
            key="${TVGName}_${EPGID}"
        fi
        ;;
    6) key=$Name ;;
    7)
        if [[ -n $Name && -n $EPGID ]]; then
            key="${Name}_${EPGID}"
        fi
        ;;
    *)
        echo "Invalid M3UKey value: $M3UKey" >&2
        ;;
    esac

    if [[ -n $key ]]; then
        generate_md5 "$key" "$M3UFileId"
    else
        echo ""
    fi
}

# Step 1: Fetch SMStreams and M3UFiles from PostgreSQL
echo "Fetching SMStreams and M3UFiles from the database..."
$PG_CMD -c "\COPY (SELECT \"Id\", \"Url\", \"CUID\", \"ChannelId\", \"EPGID\", \"TVGName\", \"Name\", \"M3UFileId\" FROM \"SMStreams\") TO '$tempFileStreams' WITH CSV HEADER DELIMITER '$delimiter';"
$PG_CMD -c "\COPY (SELECT \"Id\", COALESCE(\"M3UKey\", 0) AS \"M3UKey\" FROM \"M3UFiles\") TO '$tempFileM3UFiles' WITH CSV HEADER DELIMITER '$delimiter';"

if [[ $? -ne 0 ]]; then
    echo "Failed to fetch data from the database."
    exit 1
fi

if [[ ! -f "$tempFileStreams" || ! -s "$tempFileStreams" ]]; then
    echo "Error: Stream data file $tempFileStreams not created or is empty."
    exit 1
fi
echo "Stream data file $tempFileStreams fetched successfully."

# Step 2: Build M3UFile mapping
declare -A m3uKeyMapping
while IFS="$delimiter" read -r Id M3UKey; do
    m3uKeyMapping["$Id"]=$M3UKey

done < <(tail -n +2 "$tempFileM3UFiles") # Skip the header line

# Step 3: Process streams and generate new IDs
processedCount=0
totalCount=$(wc -l <"$tempFileStreams")
((totalCount--)) # Subtract the header line
>"$tempFileBatch"

while IFS="$delimiter" read -r Id Url CUID ChannelId EPGID TVGName Name M3UFileId; do
    # Skip the header line
    [[ "$Id" == "Id" ]] && continue

    # Handle edge cases where M3UFileId is empty or invalid
    if [[ -z "$M3UFileId" || "$M3UFileId" -lt 0 ]]; then
        M3UKey="0"
    else
        M3UKey=${m3uKeyMapping["$M3UFileId"]}
    fi

    if [[ -z $M3UKey ]]; then
        M3UKey="0"
    fi

    # Generate the new ID
    newId=$(generate_m3u_key_value "$M3UKey" "$M3UFileId" "$Url" "$CUID" "$ChannelId" "$EPGID" "$TVGName" "$Name")

    if [[ -n $newId ]]; then
        echo "$Id,$newId,$M3UFileId" >>"$tempFileBatch"
        ((processedCount++))
    fi

    # Process batch when size limit is reached
    if [[ $((processedCount % batchSize)) -eq 0 ]]; then
        echo "Updating batch of $batchSize records... (Processed: $processedCount/$totalCount)"
        $PG_CMD <<EOF
-- Step 1: Create a temporary table for batch processing
CREATE TEMP TABLE $tempTable (old_id TEXT, new_id TEXT, m3ufileid INT);

-- Step 2: Copy batch data into the temporary table
\COPY $tempTable FROM '$tempFileBatch' WITH CSV;

-- Step 3: Recreate SMStreams with new IDs
INSERT INTO "SMStreams" (
    "Id",
    "ClientUserAgent",
    "FilePosition",
    "IsHidden",
    "IsUserCreated",
    "M3UFileId",
    "ChannelNumber",
    "M3UFileName",
    "Group",
    "EPGID",
    "Logo",
    "Name",
    "Url",
    "StationId",
    "IsSystem",
    "CUID",
    "SMStreamType",
    "NeedsDelete",
    "ChannelName",
    "ChannelId",
    "CommandProfileName",
    "TVGName",
    "ExtInf"
)
SELECT 
    temp.new_id,
    streams."ClientUserAgent",
    streams."FilePosition",
    streams."IsHidden",
    streams."IsUserCreated",
    temp.m3ufileid,
    streams."ChannelNumber",
    streams."M3UFileName",
    streams."Group",
    streams."EPGID",
    streams."Logo",
    streams."Name",
    streams."Url",
    streams."StationId",
    streams."IsSystem",
    streams."CUID",
    streams."SMStreamType",
    streams."NeedsDelete",
    streams."ChannelName",
    streams."ChannelId",
    streams."CommandProfileName",
    streams."TVGName",
    streams."ExtInf"
FROM $tempTable temp
INNER JOIN "SMStreams" streams
ON temp.old_id = streams."Id";

-- Step 4: Recreate SMChannelStreamLinks with new IDs
INSERT INTO "SMChannelStreamLinks" ("SMStreamId", "SMChannelId", "Rank")
SELECT
    temp.new_id,
    links."SMChannelId",
    links."Rank"
FROM $tempTable temp
INNER JOIN "SMChannelStreamLinks" links
ON temp.old_id = links."SMStreamId";

-- Step 5: Delete old SMChannelStreamLinks
DELETE FROM "SMChannelStreamLinks"
WHERE "SMStreamId" IN (SELECT old_id FROM $tempTable);

-- Step 6: Delete old SMStreams
DELETE FROM "SMStreams"
WHERE "Id" IN (SELECT old_id FROM $tempTable);

-- Step 7: Drop the temporary table
DROP TABLE $tempTable;

EOF
        >"$tempFileBatch"
    fi
done < <(tail -n +2 "$tempFileStreams") # Skip the header line

# Step 4: Update remaining records in the batch
if [[ -s "$tempFileBatch" ]]; then
    echo "Updating final batch of records... (Processed: $processedCount/$totalCount)"
    $PG_CMD <<EOF
-- Step 1: Create a temporary table for batch processing
CREATE TEMP TABLE $tempTable (old_id TEXT, new_id TEXT, m3ufileid INT);

-- Step 2: Copy batch data into the temporary table
\COPY $tempTable FROM '$tempFileBatch' WITH CSV;

-- Step 3: Recreate SMStreams with new IDs
INSERT INTO "SMStreams" (
    "Id",
    "ClientUserAgent",
    "FilePosition",
    "IsHidden",
    "IsUserCreated",
    "M3UFileId",
    "ChannelNumber",
    "M3UFileName",
    "Group",
    "EPGID",
    "Logo",
    "Name",
    "Url",
    "StationId",
    "IsSystem",
    "CUID",
    "SMStreamType",
    "NeedsDelete",
    "ChannelName",
    "ChannelId",
    "CommandProfileName",
    "TVGName",
    "ExtInf"
)
SELECT 
    temp.new_id,
    streams."ClientUserAgent",
    streams."FilePosition",
    streams."IsHidden",
    streams."IsUserCreated",
    temp.m3ufileid,
    streams."ChannelNumber",
    streams."M3UFileName",
    streams."Group",
    streams."EPGID",
    streams."Logo",
    streams."Name",
    streams."Url",
    streams."StationId",
    streams."IsSystem",
    streams."CUID",
    streams."SMStreamType",
    streams."NeedsDelete",
    streams."ChannelName",
    streams."ChannelId",
    streams."CommandProfileName",
    streams."TVGName",
    streams."ExtInf"
FROM $tempTable temp
INNER JOIN "SMStreams" streams
ON temp.old_id = streams."Id";

-- Step 4: Recreate SMChannelStreamLinks with new IDs
INSERT INTO "SMChannelStreamLinks" ("SMStreamId", "SMChannelId", "Rank")
SELECT
    temp.new_id,
    links."SMChannelId",
    links."Rank"
FROM $tempTable temp
INNER JOIN "SMChannelStreamLinks" links
ON temp.old_id = links."SMStreamId";

-- Step 5: Delete old SMChannelStreamLinks
DELETE FROM "SMChannelStreamLinks"
WHERE "SMStreamId" IN (SELECT old_id FROM $tempTable);

-- Step 6: Delete old SMStreams
DELETE FROM "SMStreams"
WHERE "Id" IN (SELECT old_id FROM $tempTable);

-- Step 7: Drop the temporary table
DROP TABLE $tempTable;

EOF
fi

# Add the didIDMigration entry to SystemKeyValues
echo "Adding the didIDMigration entry to SystemKeyValues..."
$PG_CMD -c "INSERT INTO \"SystemKeyValues\" (\"Key\", \"Value\") VALUES ('didIDMigration', 'true');"

if [[ $? -eq 0 ]]; then
    echo "Successfully added the didIDMigration entry."
else
    echo "Failed to add the didIDMigration entry." >>"$errorFile"
    exit 1
fi

echo "Migration completed successfully. Processed $processedCount streams."
echo "Temporary files retained in $dbDir."
