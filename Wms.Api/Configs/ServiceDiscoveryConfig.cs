using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Steeltoe.Extensions.Configuration.Placeholder;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

namespace Wms.Api.Configs;

public static class ServiceDiscoveryConfig
{

    public static WebApplicationBuilder ConfigureServiceDiscovery(this WebApplicationBuilder builder)
    {
        builder.AddServiceDiscovery(options => options.UseConsul());

        return builder;
    }

}
