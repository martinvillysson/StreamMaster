﻿namespace StreamMaster.Application.Settings.Commands;

[TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
public class UpdateSettingParameters
{
    public string? STRMBaseURL { get; set; }
    public bool? AutoSetEPG { get; set; }
    public bool? BackupEnabled { get; set; }
    public int? BackupVersionsToKeep { get; set; }
    public int? BackupInterval { get; set; }
    public bool? DebugAPI { get; set; }
    public bool? UseChannelLogoForProgramLogo { get; set; }

    [TsProperty(ForceNullable = true)]
    public SDSettingsRequest? SDSettings { get; set; }

    public bool? DeleteOldSTRMFiles { get; set; }
    public bool? ShowClientHostNames { get; set; }
    public int? IconCacheExpirationDays { get; set; }
    public string? DefaultCompression { get; set; }
    public string? M3U8OutPutProfile { get; set; }
    public string? AdminPassword { get; set; }
    public string? AdminUserName { get; set; }

    public int? StreamReadTimeOutMs { get; set; }
    public int? StreamStartTimeoutMs { get; set; }
    public int? ClientReadTimeoutMs { get; set; }
    public int? StreamRetryLimit { get; set; }
    public int? StreamShutDownDelayMs { get; set; }
    public int? StreamRetryHours { get; set; }

    public string? AuthenticationMethod { get; set; }
    public bool? LogoCache { get; set; }
    public bool? CleanURLs { get; set; }
    public string? ClientUserAgent { get; set; }
    public string? DeviceID { get; set; }
    public bool? EnableSSL { get; set; }
    public bool? ShowMessageVideos { get; set; }

    public string? FFMPegExecutable { get; set; }
    public string? FFProbeExecutable { get; set; }
    public int? GlobalStreamLimit { get; set; }
    public bool? PrettyEPG { get; set; }
    public string? ShowIntros { get; set; }
    public int? MaxConnectRetry { get; set; }
    public int? MaxConnectRetryTimeMS { get; set; }
    public string? SSLCertPassword { get; set; }
    public string? SSLCertPath { get; set; }

    public string? DefaultOutputProfileName { get; set; }
    public string? DefaultCommandProfileName { get; set; }

    //public string? StreamingClientUserAgent { get; set; }
    //public string? CommandProfileName { get; set; }
    //public bool? VideoStreamAlwaysUseEPGLogo { get; set; }
    //public bool? EnablePrometheus { get; set; }
    public int? MaxLogFiles { get; set; }

    public int? MaxLogFileSizeMB { get; set; }

    [TsProperty(ForceNullable = true)]
    public List<string>? NameRegex { get; set; } = [];

    public bool? EnableShortLinks { get; set; }
}