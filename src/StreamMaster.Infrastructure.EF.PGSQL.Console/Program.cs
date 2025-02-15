using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StreamMaster.Infrastructure.EF.PGSQL;
using System;
using System.Linq;

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
    await dbContext.MigrateDatabaseAsync();
}