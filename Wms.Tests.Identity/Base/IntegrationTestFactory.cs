namespace Base;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Xunit;

public class IntegrationTestFactory<TApiMarker> : WebApplicationFactory<TApiMarker>, IAsyncLifetime where TApiMarker : class
{
    public PostgreSqlContainer DbContainer { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testcontainersettings.json", false, false)
            .AddEnvironmentVariables()
            .Build();

        DbContainer = new PostgreSqlBuilder()
         .WithDatabase(config.GetValue<string>("DbTestContainer:Database"))
            .WithUsername(config.GetValue<string>("DbTestContainer:Username"))
            .WithPassword(config.GetValue<string>("DbTestContainer:Password"))
            .WithImage(config.GetValue<string>("DbTestContainer:Image"))
            .WithCleanUp(true)
            .Build();

        await DbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await DbContainer.DisposeAsync();
    }

    // This method gets called by the runtime. Use this method to add, remove or modify services before the app starts.
    // Also, derived classes can override this method to add additional services to the container.
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request client before CreateClient() is called in the test.
    // Also, derived classes can override this method to configure the HTTP request client.
    protected override void ConfigureClient(HttpClient client)
    {
    }

    // Helper methods to use in tests

    public T? GetService<T>()
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetService<T>();
    }

    public TDbContext? GetDbContext<TDbContext>() where TDbContext : DbContext
    {
        return GetService<TDbContext>();
    }

    public async Task ClearDbTable<TDbContext>(string tableName, string schemaName = "corr") where TDbContext : DbContext
    {
        var cmd = $"TRUNCATE TABLE {schemaName}.{tableName} RESTART IDENTITY CASCADE";
        await GetDbContext<TDbContext>()!.Database.ExecuteSqlRawAsync(cmd);
    }
}
