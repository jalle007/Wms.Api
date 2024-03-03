//namespace Wms.Tests;

//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Mocks;

//// Instantiates once per test class (or once per whole test collection)
//public class WmsTestFactory : IntegrationTestFactory<IIdentityApiMarker>
//{

//    protected override void ConfigureWebHost(IWebHostBuilder builder)
//    {
//        // Override database connection string to use TestContainer database
//        builder.UseSetting("ConnectionStrings:IdentityDb", DbContainer.GetConnectionString());

//        // Override Elasticseach host to disable Serilog Elasticsearch sink
//        builder.UseSetting("Elasticsearch:Uri", "http://localhost:9200");

//        // Ensure maintenance mode is disabled
//        builder.UseSetting("Maintenance:DefaultState", "false");

//        // Set MaxFailedAccessAttempts to be used during LoginTests
//        builder.UseSetting("IdentitySettings:MaxFailedAccessAttempts", "2");

//        // Override Consul Discovery to prevent test errors from occurring
//        builder.UseSetting("Consul:Discovery:Enabled", "false");
//        builder.UseSetting("Consul:Discovery:Register", "false");
//        builder.UseSetting("Consul:Discovery:Deregister", "false");
//        builder.UseSetting("Consul:Host", "testlocalhost");

//        // Set app environment
//        builder.UseEnvironment(AppEnvironments.Test);

//        // Modify application services
//        builder.ConfigureTestServices(services =>
//        {
//         // Ensure database is created before application starts
//            services.EnsureDbCreated<IdentityDbContext>();
//        });
//    }

//}
