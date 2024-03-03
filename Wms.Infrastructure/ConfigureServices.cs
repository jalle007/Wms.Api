using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Infrastructure.Configs;
using Wms.Infrastructure.Services;
using Wms.Infrastructure.Services.AuthServices;
using Wms.Infrastructure.Services.Other;

namespace Wms.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDbContext(configuration);

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBarcodeService, BarcodeService>();

            //Models
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderTraceService, OrderTraceService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IShelfService, ShelfService>();
            services.AddScoped<ISampleService, SampleService>();
            services.AddScoped<IStorageLocationService, StorageLocationService>();
            services.AddScoped<ITransportUnitService, TransportUnitService>();
            services.AddScoped<IWarehouseService, WarehouseService>();

            return services;
        }
    }
}