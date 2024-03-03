namespace Mocks;

using Microsoft.AspNetCore.Identity;
using Wms.Infrastructure.Models;

public static class WmsMocks
{
    public static User MakeIdentityAppUser(string userName, string password)
    {
        //var user = new User
        //{
        //    Id = Guid.NewGuid().ToString(),
        //    UserName = userName,
        //    NormalizedUserName = userName.ToUpper(),
        //    Email = userName,
        //    NormalizedEmail = userName.ToUpper(),
        //    FirstName = "Test",
        //    LastName = "User",
        //    EmailConfirmed = true,
        //    LockoutEnabled = true
        //};

        //user.PasswordHash = passwordHasher.HashPassword(user, password);

        //return user;

        return null;
    }
}
