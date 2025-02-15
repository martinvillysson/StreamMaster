using Microsoft.AspNetCore.SignalR;

using StreamMaster.Application.Interfaces;
using StreamMaster.Application.Hubs;
using StreamMaster.Domain.Configuration;
using StreamMaster.Application.SMStreams.Commands;

namespace StreamMaster.Infrastructure.Services;

public partial class DataRefreshService(IHubContext<StreamMasterHub, IStreamMasterHub> hub) : IDataRefreshService
{
    public async Task RefreshAll()
    {
        await RefreshChannelGroups(alwaysRun: true);
        await RefreshCustom(alwaysRun: true);
        await RefreshEPGFiles(alwaysRun: true);
        await RefreshEPGs(alwaysRun: true);
        await RefreshLogos(alwaysRun: true);
        await RefreshLogs(alwaysRun: true);
        await RefreshM3UFiles(alwaysRun: true);
        await RefreshM3UGroups(alwaysRun: true);
        await RefreshProfiles(alwaysRun: true);
        await RefreshSchedulesDirect(alwaysRun: true);
        await RefreshSettings(alwaysRun: true);
        await RefreshSMChannelChannelLinks(alwaysRun: true);
        await RefreshSMChannels(alwaysRun: true);
        await RefreshSMChannelStreamLinks(alwaysRun: true);
        await RefreshSMStreams(alwaysRun: true);
        await RefreshSMTasks(alwaysRun: true);
        await RefreshStatistics(alwaysRun: true);
        await RefreshStreamGroups(alwaysRun: true);
        await RefreshStreamGroupSMChannelLinks(alwaysRun: true);
        await RefreshUserGroups(alwaysRun: true);
        await RefreshVs(alwaysRun: true);
    }

    public async Task RefreshChannelGroups(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetChannelGroups");
            await hub.Clients.All.DataRefresh("GetChannelGroupsFromSMChannels");
            await hub.Clients.All.DataRefresh("GetPagedChannelGroups");
        }
    }

    public async Task RefreshCustom(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetCustomPlayLists");
            await hub.Clients.All.DataRefresh("GetIntroPlayLists");
        }
    }

    public async Task RefreshEPGFiles(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetEPGFileNames");
            await hub.Clients.All.DataRefresh("GetEPGFiles");
            await hub.Clients.All.DataRefresh("GetEPGNextEPGNumber");
            await hub.Clients.All.DataRefresh("GetPagedEPGFiles");
        }
    }

    public async Task RefreshEPGs(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetEPGColors");
        }
    }

    public async Task RefreshLogos(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetCustomLogos");
            await hub.Clients.All.DataRefresh("GetLogos");
        }
    }

    public async Task RefreshLogs(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetLogNames");
        }
    }

    public async Task RefreshM3UFiles(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetM3UFileNames");
            await hub.Clients.All.DataRefresh("GetM3UFiles");
            await hub.Clients.All.DataRefresh("GetPagedM3UFiles");
        }
    }

    public async Task RefreshM3UGroups(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetM3UGroups");
            await hub.Clients.All.DataRefresh("GetPagedM3UGroups");
        }
    }

    public async Task RefreshProfiles(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetCommandProfiles");
            await hub.Clients.All.DataRefresh("GetOutputProfiles");
        }
    }

    public async Task RefreshSchedulesDirect(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetAvailableCountries");
            await hub.Clients.All.DataRefresh("GetHeadendsToView");
            await hub.Clients.All.DataRefresh("GetSDReady");
            await hub.Clients.All.DataRefresh("GetSelectedStationIds");
            await hub.Clients.All.DataRefresh("GetStationChannelNames");
            await hub.Clients.All.DataRefresh("GetStationPreviews");
            await hub.Clients.All.DataRefresh("GetSubScribedHeadends");
            await hub.Clients.All.DataRefresh("GetSubscribedLineups");
        }
    }

    public async Task RefreshSettings(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetSettings");
        }
    }

    public async Task RefreshSMChannelChannelLinks(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetSMChannelChannels");
        }
    }

    public async Task RefreshSMChannels(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetPagedSMChannels");
        }
    }

    public async Task RefreshSMChannelStreamLinks(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetSMChannelStreams");
        }
    }

    public async Task RefreshSMStreams(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetPagedSMStreams");
        }
    }

    public async Task RefreshSMTasks(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetSMTasks");
        }
    }

    public async Task RefreshStatistics(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetChannelMetrics");
            await hub.Clients.All.DataRefresh("GetDownloadServiceStatus");
            await hub.Clients.All.DataRefresh("GetIsSystemReady");
            await hub.Clients.All.DataRefresh("GetSystemStatus");
            await hub.Clients.All.DataRefresh("GetTaskIsRunning");
            await hub.Clients.All.DataRefresh("GetVideoInfos");
        }
    }

    public async Task RefreshStreamGroups(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetPagedStreamGroups");
            await hub.Clients.All.DataRefresh("GetStreamGroupProfiles");
            await hub.Clients.All.DataRefresh("GetStreamGroups");
        }
    }

    public async Task RefreshStreamGroupSMChannelLinks(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetStreamGroupSMChannels");
        }
    }

    public async Task RefreshUserGroups(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetPagedUserGroups");
            await hub.Clients.All.DataRefresh("GetUserGroups");
        }
    }

    public async Task RefreshVs(bool alwaysRun = false)
    {
        if (alwaysRun || BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetVs");
        }
    }

    public async Task AuthLogOut()
    {
        await hub.Clients.All.AuthLogOut();
    }

    public async Task IsSystemReady()
    {
        await hub.Clients.All.IsSystemReady(BuildInfo.IsSystemReady);
    }

    public async Task TaskIsRunning()
    {
        await hub.Clients.All.TaskIsRunning(BuildInfo.IsTaskRunning);
    }

    public async Task RefreshVideoInfos()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }

        await hub.Clients.All.DataRefresh("GetVideoInfos");
    }

    public async Task RefreshOutputProfiles()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await hub.Clients.All.DataRefresh("GetOutputProfiles");
    }

    public async Task RefreshCommandProfiles()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await hub.Clients.All.DataRefresh("GetCommandProfiles");
    }

    public async Task RefreshDownloadServiceStatus()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await hub.Clients.All.DataRefresh("GetDownloadServiceStatus");
    }

    public async Task SendStatus(ImageDownloadServiceStatus imageDownloadServiceStatus)
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await hub.Clients.All.SendStatus(imageDownloadServiceStatus);
    }

    public async Task RefreshAllGroups()
    {
        if (BuildInfo.IsSystemReady)
        {
            await RefreshM3UGroups();
            await RefreshUserGroups();
        }
    }

    public async Task RefreshStationPreviews()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await hub.Clients.All.DataRefresh("GetStationPreviews");
    }

    public async Task RefreshSelectedStationIds()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await hub.Clients.All.DataRefresh("GetSelectedStationIds");
    }

    public async Task Refresh(string command)
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }

        await hub.Clients.All.DataRefresh(command);
    }

    public async Task RefreshAllEPG()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await RefreshEPGFiles();
        await RefreshEPGs();
        await hub.Clients.All.DataRefresh("GetStationChannelNames");
    }

    public async Task RefreshAllM3U()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await RefreshM3UFiles();
        await RefreshSMStreams();
        await RefreshSMChannels();
        await RefreshSMChannelStreamLinks();
        await RefreshChannelGroups();
    }

    public async Task RefreshSDReady()
    {
        if (BuildInfo.IsSystemReady)
        {
            await hub.Clients.All.DataRefresh("GetSDReady");
        }
    }

    public async Task RefreshAllSMChannels()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await RefreshSMChannels();
        await RefreshSMChannelStreamLinks();
        await RefreshStreamGroupSMChannelLinks();
        await RefreshChannelGroups();
    }

    public async Task ClearByTag(string Entity, string Tag)
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }

        await hub.Clients.All.ClearByTag(new ClearByTag(Entity, Tag));
    }

    public async Task RefreshEPGColors()
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }
        await hub.Clients.All.DataRefresh("GetEPGColors");
    }

    public async Task SetField(List<FieldData> fieldData)
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }

        await hub.Clients.All.SetField(fieldData);
    }

    public async Task SendSMTasks(List<SMTask> smTasks)
    {
        //if (!BuildInfo.IsSystemReady)
        //{
        //    return;
        //}

        await hub.Clients.All.SendSMTasks(smTasks);
    }

    public async Task SendMessage(SMMessage smMessage)
    {
        if (!BuildInfo.IsSystemReady)
        {
            return;
        }

        await hub.Clients.All.SendMessage(smMessage);
    }
}