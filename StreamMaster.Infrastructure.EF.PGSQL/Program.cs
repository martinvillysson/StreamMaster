using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StreamMaster.Infrastructure.EF.PGSQL;

var services = new ServiceCollection();

// Add logging
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

// Add DbContext
services.AddDbContext<PGSQLRepositoryContext>(options =>
{
    options.UseNpgsql(PGSQLRepositoryContext.DbConnectionString,
        o =>
        {
            o.UseNodaTime();
            o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });
});

var serviceProvider = services.BuildServiceProvider();

// Get the DbContext and run migrations
using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PGSQLRepositoryContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<PGSQLRepositoryContext>>();

    try
    {
        try
        {
            var appliedMigrations = await dbContext.Database.GetAppliedMigrationsAsync();
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            logger.LogInformation($"Current migration: {appliedMigrations.LastOrDefault() ?? "none"}");
            logger.LogInformation($"Pending migrations: {string.Join(", ", pendingMigrations)}");

            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying migrations");
            throw;
        }

        dbContext.ApplyCustomSqlScripts();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database initialization");
        throw;
    }
}