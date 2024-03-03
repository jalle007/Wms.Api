using Microsoft.AspNetCore.Identity;
using Wms.Infrastructure.Extensions;
using static Wms.Infrastructure.Enums;
using static Wms.Infrastructure.Extensions.EnumExtensions;

public class EnsureRolesExistFilter : IStartupFilter
{
    private readonly IServiceProvider _serviceProvider;

    public EnsureRolesExistFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            // Get all roles from the RoleType enum 
            var roleNames = EnumExtensions.Names(typeof(RoleType)).ToList();

            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    var role = new IdentityRole { Name = roleName };
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {result.Errors.First().Description}");
                    }
                }
            }

        }

        return next;
    }
}
