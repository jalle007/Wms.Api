namespace Wms.Infrastructure.Configs;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DbContextConfig
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = typeof(WmsDbContext).Assembly.FullName!.Split(',')[0];

        var connectionString = configuration.GetConnectionString("WmsDatabase");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Missing valid connectionString");
        }

        services.AddDbContext<WmsDbContext>(builder =>
        {
            builder.UseNpgsql(connectionString, options =>
                {
                    //options.UseNodaTime();
                    //options.MigrationsAssembly(migrationsAssembly);
                    //options.MigrationsHistoryTable(migrationsHistoryTable, migrationsHistorySchema);
                });
        });

        return services;
    }
}
