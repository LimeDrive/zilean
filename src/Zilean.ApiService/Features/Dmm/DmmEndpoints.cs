using Zilean.Database.Services;
using Zilean.Shared.Features.Dmm;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Zilean.ApiService.Features.Dmm;

public static class DmmEndpoints
{
    private const string GroupName = "dmm";
    private const string Search = "/search";
    private const string Filtered = "/filtered";
    private const string Ingest = "/on-demand-scrape";

    public static WebApplication MapDmmEndpoints(this WebApplication app, ZileanConfiguration configuration)
    {
        if (configuration.Dmm.EnableEndpoint)
        {
            app.MapGroup(GroupName)
                .WithTags(GroupName)
                .Dmm()
                .DisableAntiforgery()
                .AllowAnonymous();
        }

        return app;
    }

    private static RouteGroupBuilder Dmm(this RouteGroupBuilder group)
    {
        group.MapPost(Search, PerformSearch)
            .Produces<ClearTorrentInfo[]>();

        group.MapGet(Filtered, PerformFilteredSearch)
            .Produces<ClearTorrentInfo[]>();

        group.MapGet(Ingest, PerformOnDemandScrape);

        return group;
    }

    private static async Task PerformOnDemandScrape(HttpContext context, ILogger<GeneralInstance> logger, IShellExecutionService executionService, ILogger<DmmSyncJob> syncLogger, IMutex mutex, DmmSyncOnDemandState state)
    {
        if (state.IsRunning)
        {
            logger.LogWarning("On-demand scrape already running.");
            return;
        }

        logger.LogInformation("Trying to schedule on-demand scrape with a 5 minute timeout on lock acquisition.");

        bool available = mutex.TryGetLock(nameof(DmmSyncJob), 1);

        if(available)
        {
            try
            {
                logger.LogInformation("On-demand scrape mutex lock acquired.");
                state.IsRunning = true;
                await new DmmSyncJob(executionService, syncLogger).Invoke();
            }
            finally
            {
                mutex.Release(nameof(DmmSyncJob));
                state.IsRunning = false;
            }

            return;
        }

        logger.LogWarning("Failed to acquire lock for on-demand scrape.");
    }

    private static async Task<Ok<ClearTorrentInfo[]>> PerformSearch(HttpContext context, ITorrentInfoService torrentInfoService, ZileanConfiguration configuration, ILogger<DmmUnfilteredInstance> logger, [FromBody] DmmQueryRequest queryRequest)
    {
        try
        {
            if (string.IsNullOrEmpty(queryRequest.QueryText))
            {
                return TypedResults.Ok(Array.Empty<ClearTorrentInfo>());
            }

            logger.LogInformation("Performing unfiltered search for {QueryText}", queryRequest.QueryText);

            var results = await torrentInfoService.SearchForTorrentInfoByOnlyTitle(queryRequest.QueryText);
            var clearResults = results.Select(ClearTorrentInfo.FromTorrentInfo).ToArray();

            logger.LogInformation("Unfiltered search for {QueryText} returned {Count} results", queryRequest.QueryText, clearResults.Length);

            return clearResults.Length == 0
                ? TypedResults.Ok(Array.Empty<ClearTorrentInfo>())
                : TypedResults.Ok(clearResults);
        }
        catch
        {
            return TypedResults.Ok(Array.Empty<ClearTorrentInfo>());
        }
    }

    private static async Task<Ok<ClearTorrentInfo[]>> PerformFilteredSearch(HttpContext context, ITorrentInfoService torrentInfoService, ZileanConfiguration configuration, ILogger<DmmFilteredInstance> logger, [AsParameters] DmmFilteredRequest request)
    {
        try
        {
            logger.LogInformation("Performing filtered search for {@Request}", request);

            var results = await torrentInfoService.SearchForTorrentInfoFiltered(new TorrentInfoFilter
            {
                Query = request.Query,
                Season = request.Season,
                Episode = request.Episode,
                Year = request.Year,
                Language = request.Language,
                Resolution = request.Resolution,
                ImdbId = request.ImdbId
            });

            var clearResults = results.Select(ClearTorrentInfo.FromTorrentInfo).ToArray();

            logger.LogInformation("Filtered search for {QueryText} returned {Count} results", request.Query, clearResults.Length);

            return clearResults.Length == 0
                ? TypedResults.Ok(Array.Empty<ClearTorrentInfo>())
                : TypedResults.Ok(clearResults);
        }
        catch
        {
            return TypedResults.Ok(Array.Empty<ClearTorrentInfo>());
        }
    }

    private abstract class DmmUnfilteredInstance;
    private abstract class DmmFilteredInstance;
    private abstract class GeneralInstance;
}