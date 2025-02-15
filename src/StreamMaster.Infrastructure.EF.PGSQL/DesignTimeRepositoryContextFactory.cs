using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StreamMaster.Infrastructure.EF.PGSQL;

public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<PGSQLRepositoryContext>
{
    public PGSQLRepositoryContext CreateDbContext(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        services.AddDbContext<PGSQLRepositoryContext>(options => options.UseNpgsql(PGSQLRepositoryContext.DbConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        var optionsBuilder = new DbContextOptionsBuilder<PGSQLRepositoryContext>();
        optionsBuilder.UseNpgsql(PGSQLRepositoryContext.DbConnectionString,
            o =>
            {
                o.UseNodaTime();
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));

        var serviceProvider = services.BuildServiceProvider();

        PGSQLRepositoryContext? context = services.BuildServiceProvider().GetService<PGSQLRepositoryContext>();
        var logger = serviceProvider.GetRequiredService<ILogger<PGSQLRepositoryContext>>();

        return new PGSQLRepositoryContext(optionsBuilder.Options, logger);
    }
}