namespace Wms.Tests.Mocks;

using Microsoft.AspNetCore.Identity;
using Wms.Infrastructure.Models;

public static class USerMocks
{
    public static User MakeAppUser(string userName, string password, IPasswordHasher<User> passwordHasher)
    {
        var user = new User
        {
            UserName = "TestUser",
            Password = "string",
            FirstName = "string",
            LastName = "string",
            Email = "string",
            Role = Infrastructure.Enums.RoleType.AdminRole,
            EmailConfirmed = true,
            PasswordHash = null,
            SecurityStamp = "string",
            ConcurrencyStamp = "string",
            PhoneNumber = "string",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = true,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        };

        user.PasswordHash = passwordHasher.HashPassword(user, password);

        return user;
    }
}
