using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StreamMaster.Domain.Helpers;
using StreamMaster.Domain.Repository;
using StreamMaster.Infrastructure.Authentication;
using StreamMaster.Infrastructure.EF.Repositories;
using StreamMaster.Infrastructure.Logger;
using StreamMaster.Infrastructure.Middleware;
using StreamMaster.Infrastructure.Services;
using StreamMaster.Infrastructure.Services.Downloads;
using StreamMaster.Infrastructure.Services.Frontend.Mappers;
using StreamMaster.SchedulesDirect.Domain.Interfaces;

namespace StreamMaster.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddScoped<IAPIKeyService, APIKeyService>();
        services.AddScoped<IAPIKeyRepository, APIKeyRepository>();
        services.AddSingleton<ILogoService, LogoService>();
        services.AddSingleton<IImageDownloadQueue, ImageDownloadQueue>();
        services.AddSingleton<ICacheableSpecification, CacheableSpecification>();
        services.AddSingleton<IJobStatusService, JobStatusService>();
        services.AddSingleton<IEPGHelper, EPGHelper>();
        services.AddSingleton<IFileLoggingServiceFactory, FileLoggingServiceFactory>();
        services.AddSingleton<IMessageService, MessageService>();
        services.AddSingleton<IDataRefreshService, DataRefreshService>();
        services.AddSingleton<IFileUtilService, FileUtilService>();

        _ = services.AddAutoMapper(
            Assembly.Load("StreamMaster.Domain"),
            Assembly.Load("StreamMaster.Application"),
            Assembly.Load("StreamMaster.Infrastructure"),
            Assembly.Load("StreamMaster.Streams"),
             Assembly.Load("StreamMaster.Streams.Domain")
        );

        _ = services.AddMediatR(cfg =>
        {
            _ = cfg.RegisterServicesFromAssemblies(
                Assembly.Load("StreamMaster.Domain"),
                Assembly.Load("StreamMaster.Application"),
                Assembly.Load("StreamMaster.Infrastructure"),
                Assembly.Load("StreamMaster.Streams"),
                 Assembly.Load("StreamMaster.Streams.Domain")
            );
        });
        return services;
    }

    public static IServiceCollection AddInfrastructureServicesEx(this IServiceCollection services)
    {
        _ = services.AddSingleton<IBroadcastService, BroadcastService>();

        _ = services.AddHostedService<TimerService>();

        // Dynamically find and register services implementing IMapHttpRequestsToDisk
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> mapHttpRequestsToDiskImplementations = assembly.GetTypes()
            .Where(type => typeof(IMapHttpRequestsToDisk).IsAssignableFrom(type) && !type.IsInterface);

        foreach (Type? implementation in mapHttpRequestsToDiskImplementations)
        {
            if (implementation.Name.EndsWith("Base"))
            {
                continue;
            }
            _ = services.AddSingleton(typeof(IMapHttpRequestsToDisk), implementation);
        }

        services.AddSingleton<IImageDownloadService, ImageDownloadService>();
        services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<IImageDownloadService>());

        return services;
    }
}