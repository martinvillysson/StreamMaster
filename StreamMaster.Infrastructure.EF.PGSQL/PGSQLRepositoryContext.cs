using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using StreamMaster.Domain.Configuration;
using StreamMaster.Infrastructure.EF.Base;

namespace StreamMaster.Infrastructure.EF.PGSQL
{
    public partial class PGSQLRepositoryContext(DbContextOptions<PGSQLRepositoryContext> options, ILogger<PGSQLRepositoryContext> logger) : BaseRepositoryContext(options)
    {
        public static string DbConnectionString => $"Host={BuildInfo.DBHost};Database={BuildInfo.DBName};Username={BuildInfo.DBUser};Password={BuildInfo.DBPassword}";

        /// <summary>
        /// Executes all SQL scripts from the "Scripts" folder in alphabetical order.
        /// </summary>
        /// <exception cref="FileNotFoundException">Thrown when the "Scripts" directory does not exist or no .sql files are found.</exception>
        public void ApplyCustomSqlScripts()
        {
            string scriptsDirectory = Path.Combine(AppContext.BaseDirectory, "Scripts");

            if (!Directory.Exists(scriptsDirectory))
            {
                throw new FileNotFoundException($"SQL scripts directory not found: {scriptsDirectory}");
            }

            List<string> sqlFiles = [.. Directory.GetFiles(scriptsDirectory, "*.sql").OrderBy(Path.GetFileName)];

            if (sqlFiles.Count == 0)
            {
                throw new FileNotFoundException($"No SQL script files found in directory: {scriptsDirectory}");
            }

            foreach (string filePath in sqlFiles)
            {
                string scriptContent = File.ReadAllText(filePath);

                // Log or indicate the file being executed
                //Console.WriteLine($"Executing script: {Path.GetFileName(filePath)}");
                logger.LogInformation($"Executing script: {Path.GetFileName(filePath)}");

                try
                {
                    // Execute the SQL script
                    Database.ExecuteSqlRaw(scriptContent);
                }
                catch (Exception ex)
                {
                    // Log error and rethrow or handle it based on your requirements
                    Console.Error.WriteLine($"Error executing script {Path.GetFileName(filePath)}: {ex.Message}");
                    throw;
                }
            }
        }
    }
}