namespace StreamMaster.Domain.Services
{
    public interface IDataRefreshService
    {
        Task SetField(List<FieldData> fieldData);

        Task ClearByTag(string Entity, string Tag);

        Task RefreshAll();

        Task Refresh(string command);

        Task RefreshDownloadServiceStatus();

        Task RefreshVs(bool alwaysRun = false);

        Task RefreshUserGroups(bool alwaysRun = false);

        Task RefreshStreamGroups(bool alwaysRun = false);

        Task RefreshStreamGroupSMChannelLinks(bool alwaysRun = false);

        Task RefreshStatistics(bool alwaysRun = false);

        Task RefreshSMTasks(bool alwaysRun = false);

        Task RefreshSMStreams(bool alwaysRun = false);

        Task RefreshSMChannels(bool alwaysRun = false);

        Task RefreshSMChannelStreamLinks(bool alwaysRun = false);

        Task RefreshSMChannelChannelLinks(bool alwaysRun = false);

        Task RefreshSettings(bool alwaysRun = false);

        Task RefreshSchedulesDirect(bool alwaysRun = false);

        Task RefreshProfiles(bool alwaysRun = false);

        Task RefreshM3UGroups(bool alwaysRun = false);

        Task RefreshM3UFiles(bool alwaysRun = false);

        Task RefreshLogs(bool alwaysRun = false);

        Task RefreshLogos(bool alwaysRun = false);

        Task RefreshEPGs(bool alwaysRun = false);

        Task RefreshEPGFiles(bool alwaysRun = false);

        Task RefreshCustom(bool alwaysRun = false);

        Task RefreshChannelGroups(bool alwaysRun = false);

        Task RefreshSDReady();

        Task RefreshAllGroups();

        Task AuthLogOut();

        Task SendStatus(ImageDownloadServiceStatus imageDownloadServiceStatus);

        Task RefreshEPGColors();

        Task RefreshSelectedStationIds();

        Task RefreshStationPreviews();

        Task RefreshOutputProfiles();

        Task RefreshCommandProfiles();

        Task IsSystemReady();

        Task TaskIsRunning();

        Task SendMessage(SMMessage smMessage);

        Task SendSMTasks(List<SMTask> smTasks);

        Task RefreshAllEPG();

        Task RefreshAllM3U();

        Task RefreshAllSMChannels();

        Task RefreshVideoInfos();
    }
}