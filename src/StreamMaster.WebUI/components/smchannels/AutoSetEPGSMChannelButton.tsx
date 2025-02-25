import SMButton from "@components/sm/SMButton";
import { useQueryFilter } from "@lib/redux/hooks/queryFilter";
import type { AutoSetEPGFromParametersRequest } from "@lib/smAPI/smapiTypes";
import { AutoSetEPGFromParameters } from "@lib/smAPI/SMChannels/SMChannelsCommands";
import React from "react";

const AutoSetEPGSMChannelButton = () => {
	const { queryFilter } = useQueryFilter("streameditor-SMChannelDataSelector");

	const handleClick = React.useCallback(async () => {
		if (!queryFilter) {
			return;
		}

		const request = {
			Parameters: queryFilter,
		} as AutoSetEPGFromParametersRequest;

		try {
			await AutoSetEPGFromParameters(request);
		} catch (error) {
			console.error("Auto Set EPG Error:", error);
		}
	}, [queryFilter]);

	return (
		<SMButton
			icon="pi-sparkles"
			iconFilled
			buttonClassName="icon-blue"
			onClick={handleClick}
			tooltip="Auto Set All EPG"
		/>
	);
};

AutoSetEPGSMChannelButton.displayName = "AutoSetEPGSMChannelButton";

export default React.memo(AutoSetEPGSMChannelButton);
