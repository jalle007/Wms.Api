namespace Wms.Tests.Tests.User;

using Wms.Infrastructure.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mocks;
using System.Net.Http.Json;
using System.Net;
using Wms.Infrastructure;
using Wms.Tests.Base;
using Xunit;
using Newtonsoft.Json;
using System.Text;

[Collection("WmsTestsCollection")]
public class UserTests : IAsyncLifetime
{
    private readonly WmsTestFactory _appFactory;
    private readonly WmsDbContext _dbContext;
    private readonly HttpClient _client;


    public UserTests(WmsTestFactory appFactory)
    {
        _appFactory = appFactory;
        _dbContext = appFactory.GetDbContext<WmsDbContext>()!;
        _client = appFactory.CreateClient();
    }

    public async Task InitializeAsync()
    {
         
    }

    public async Task DisposeAsync()
    {
        await _appFactory.ClearDbTable<WmsDbContext>(WmsDbContext.TableUsers);
    }



    [Fact(Skip ="skip test for now")]
    public async Task CreateUser_ReturnsCreatedUser()
    {
        // Arrange
        var newUser = new User
        {
            UserName = "TestUser",
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "Password",
            Role = Enums.RoleType.AdminRole
            // Add other fields as required
        };

        var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/v1/user", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var obj = await response.Content.ReadAsStringAsync();
        var returnedUser = JsonConvert.DeserializeObject<User>(obj);
        Assert.Equal(newUser.UserName, returnedUser.UserName);
        Assert.Equal(newUser.Email, returnedUser.Email);
        // Add other asserts as required
    }




}
