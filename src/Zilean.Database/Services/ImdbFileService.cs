namespace Zilean.Database.Services;

public class ImdbFileService(ILogger<ImdbFileService> logger, ZileanConfiguration configuration, IServiceProvider serviceProvider) : BaseDapperService(logger, configuration), IImdbFileService
{
    private ConcurrentBag<ImdbFile> ImdbFiles { get; } = [];

    public void AddImdbFile(ImdbFile imdbFile) => ImdbFiles.Add(imdbFile);
    public async Task StoreImdbFiles()
    {
        await using var serviceScope = serviceProvider.CreateAsyncScope();
        await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ZileanDbContext>();

        if (ImdbFiles.IsEmpty)
        {
            logger.LogInformation("No imdb files to store.");
            return;
        }

        var bulkConfig = new BulkConfig
        {
            SetOutputIdentity = false,
            BatchSize = 5000,
            PropertiesToIncludeOnUpdate = [string.Empty],
            UpdateByProperties = ["ImdbId"],
            BulkCopyTimeout = 0,
            NotifyAfter = (int)Math.Ceiling(ImdbFiles.Count * 0.05),
            TrackingEntities = false,
        };

        dbContext.Database.SetCommandTimeout(0);

        logger.LogInformation("Storing {Count} imdb entries", ImdbFiles.Count);
        await dbContext.BulkInsertOrUpdateAsync(ImdbFiles, bulkConfig, WriteProgress);
    }

    public async Task<ImdbSearchResult[]> SearchForImdbIdAsync(string query, int? year = null, string? category = null) =>
        await ExecuteCommandAsync(async connection =>
        {
            const string sql =
                """
                SELECT
                    imdb_id as "ImdbId",
                    title as "Title",
                    year as "Year",
                    score as "Score",
                    category as "Category"
                FROM search_imdb_meta(@query, @category, @year, 10)
                """;

            var parameters = new DynamicParameters();

            parameters.Add("@query", query);
            parameters.Add("@category", category);
            parameters.Add("@year", year);

            var result = await connection.QueryAsync<ImdbSearchResult>(sql, parameters);

            return result.ToArray();
        }, "Error finding imdb metadata.");

    private void WriteProgress(decimal @decimal) => logger.LogInformation("Storing imdb meta info: {Percentage:P}", @decimal);
}
