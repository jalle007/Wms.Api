namespace Wms.Tests.Base;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Wms.Infrastructure;


// Instantiates once per test class (or once per whole test collection)
public class WmsTestFactory : IntegrationTestFactory<IIdentityApiMarker>
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Override database connection string to use TestContainer database
        builder.UseSetting("ConnectionStrings:WmsDatabase", DbContainer.GetConnectionString());

        // Override Elasticseach host to disable Serilog Elasticsearch sink
        //builder.UseSetting("Elasticsearch:Uri", "http://localhost:9200");

        // Ensure maintenance mode is disabled
        //builder.UseSetting("Maintenance:DefaultState", "false");

        // Set MaxFailedAccessAttempts to be used during LoginTests
        //builder.UseSetting("IdentitySettings:MaxFailedAccessAttempts", "2");

        // Set app environment
        //builder.UseEnvironment(AppEnvironments.Test);

        // Modify application services
        builder.ConfigureTestServices(services =>
        {
            // Ensure database is created before application starts
            services.EnsureDbCreated<WmsDbContext>();
        });
    }

}
