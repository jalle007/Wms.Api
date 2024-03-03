using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Steeltoe.Extensions.Configuration.Placeholder;

namespace Wms.Api.Configs;

public static class AppSettingsConfig
{

    public static WebApplicationBuilder ConfigureAppSettings(this WebApplicationBuilder builder, string[] args)
    {
        var contentRootPath = builder.Environment.ContentRootPath;
        var environmentName = builder.Environment.EnvironmentName;

        builder.Configuration
            .RemoveDefaultConfigSources()
            .SetBasePath(contentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.override.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddPlaceholderResolver();

        if (args.Length > 0)
        {
            builder.Configuration.AddCommandLine(args);
        }
        
        return builder;
    }

    private static IConfigurationBuilder RemoveDefaultConfigSources(this IConfigurationBuilder config)
    {
        // Preserve 'launchSettings' config source but remove any other initially loaded config sources
        var sourceTypesToRemove = new List<Type>
        {
            typeof(JsonConfigurationSource), typeof(EnvironmentVariablesConfigurationSource)
        };

        config.Sources
            .Where(x => sourceTypesToRemove.Contains(x.GetType()))
            .ToList()
            .ForEach(x => config.Sources.Remove(x));

        return config;
    }
}
