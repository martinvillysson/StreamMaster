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
    dbContext.Database.Migrate();
    dbContext.MigrateData();
}