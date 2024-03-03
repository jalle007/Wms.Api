namespace Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class TestExtensions
{
    public static IServiceCollection RemoveDbContext<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
    {
        services.RemoveAll(typeof(DbContextOptions<TDbContext>));
        return services;
    }

    public static void EnsureDbCreated<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<TDbContext>();
        dbContext.Database.Migrate();
    }
}
