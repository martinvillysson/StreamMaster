﻿using System.Collections.Concurrent;
using System.Text.Json;

using StreamMaster.Domain.Cache;
using StreamMaster.SchedulesDirect.Domain;

namespace StreamMaster.SchedulesDirect.Images;

public class SportsImages(
    ILogger<SportsImages> logger,
    SMCacheManager<SportsImages> hybridCache,
    IImageDownloadQueue imageDownloadQueue,
    IOptionsMonitor<SDSettings> sdSettings,
    IProgramRepository programRepository,
    ISchedulesDirectAPIService schedulesDirectAPI) : ISportsImages, IDisposable
{
    private static readonly SemaphoreSlim classSemaphore = new(1, 1);
    private readonly SemaphoreSlim semaphore = new(SDAPIConfig.MaxParallelDownloads);

    public List<MxfProgram> SportEvents { get; set; } = [];

    public async Task<bool> ProcessArtAsync(CancellationToken cancellationToken)
    {
        if (!sdSettings.CurrentValue.SportsImages)
        {
            return true;
        }

        await classSemaphore.WaitAsync(cancellationToken);
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }
        try
        {
            if (!sdSettings.CurrentValue.SportsImages)
            {
                return true;
            }

            logger.LogInformation("Entering GetAllSportsImages() for {totalObjects} sports events.", SportEvents.Count);

            List<string> sportsImageQueue = [];
            foreach (MxfProgram sportEvent in SportEvents)
            {
                if (!string.IsNullOrEmpty(sportEvent.MD5))
                {
                    List<ProgramArtwork>? artWorks = await hybridCache.GetAsync<List<ProgramArtwork>>(sportEvent.MD5);

                    if (artWorks is not null)
                    {
                        //sportEvent.AddArtworks(artWorks);
                        programRepository.SetProgramLogos(sportEvent, artWorks);
                        imageDownloadQueue.EnqueueProgramArtworkCollection(artWorks);
                    }
                }
                else
                {
                    sportsImageQueue.Add(sportEvent.ProgramId);
                }
            }

            logger.LogDebug("Found {sportsImageQueueCount} cached/unavailable sport event image links.", sportsImageQueue.Count);

            if (sportsImageQueue.Count > 0)
            {
                ConcurrentBag<ProgramMetadata> sportsImageResponses = [];
                await DownloadAndProcessImagesAsync(sportsImageQueue, sportsImageResponses, cancellationToken).ConfigureAwait(false);
                await ProcessSportsImageResponsesAsync(sportsImageResponses);
            }

            logger.LogInformation("Exiting GetAllSportsImages(). SUCCESS.");
            //await hybridCache.ClearAsync();
            return true;
        }
        finally
        {
            classSemaphore.Release();
        }
    }

    private async Task DownloadAndProcessImagesAsync(List<string> sportsImageQueue, ConcurrentBag<ProgramMetadata> sportsImageResponses, CancellationToken cancellationToken)
    {
        List<Task> tasks = [];
        int processedCount = 0;

        for (int i = 0; i <= sportsImageQueue.Count / SDAPIConfig.MaxImgQueries; i++)
        {
            int startIndex = i * SDAPIConfig.MaxImgQueries;
            tasks.Add(Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                try
                {
                    int itemCount = Math.Min(sportsImageQueue.Count - startIndex, SDAPIConfig.MaxImgQueries);
                    await schedulesDirectAPI.DownloadImageResponsesAsync(sportsImageQueue, sportsImageResponses, startIndex, cancellationToken).ConfigureAwait(false);
                    Interlocked.Add(ref processedCount, itemCount);
                    logger.LogInformation("Downloaded sport event images {ProcessedCount} of {TotalCount}", processedCount, sportsImageQueue.Count);
                }
                finally
                {
                    semaphore.Release();
                }
            }, cancellationToken));
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    private async Task ProcessSportsImageResponsesAsync(ConcurrentBag<ProgramMetadata> sportsImageResponses)
    {
        string artworkSize = string.IsNullOrEmpty(sdSettings.CurrentValue.ArtworkSize) ? BuildInfo.DefaultSDImageSize : sdSettings.CurrentValue.ArtworkSize;

        foreach (ProgramMetadata response in sportsImageResponses)
        {
            if (response.Data == null || response.Data.Count == 0 || response.Data[0].Code != 0)
            {
                logger.LogWarning("No Sport Image artwork found for {ProgramId}", response.ProgramId);
                continue;
            }

            MxfProgram? sportEvent = SportEvents.FirstOrDefault(arg => arg.ProgramId == response.ProgramId);
            if (sportEvent == null)
            {
                continue;
            }

            if (!response.ProgramId.StartsWith("SP"))
            {
                continue;
            }

            List<ProgramArtwork> artworks = SDHelpers.GetTieredImages(response.Data, artworkSize, ["team event", "episode", "series", "sport"], sdSettings.CurrentValue.MoviePosterAspect);
            //sportEvent.AddArtworks(artworks);

            await hybridCache.SetAsync(sportEvent.MD5, JsonSerializer.Serialize(artworks));

            if (artworks.Count > 0)
            {
                programRepository.SetProgramLogos(sportEvent, artworks);
                imageDownloadQueue.EnqueueProgramArtworkCollection(artworks);
            }
            else
            {
                logger.LogWarning("No artwork found for {ProgramId}", response.ProgramId);
            }
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            semaphore.Dispose();
        }
    }

    public List<string> GetExpiredKeys()
    {
        return hybridCache.GetExpiredKeysAsync().Result;
    }

    public void RemovedExpiredKeys(List<string>? keysToDelete = null)
    {
    }
}