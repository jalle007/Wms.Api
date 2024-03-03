namespace Wms.Tests.Controllers.WMSController;

using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wms.Tests;
using Xunit;

[Collection("IdentityTestsCollection")]
public class LoginTests : IAsyncLifetime
{
    //private readonly WmsTestFactory _appFactory;
    //private readonly IdentityDbContext _dbContext;
    private readonly HttpClient _client;

    //public LoginTests(WmsTestFactory appFactory)
    //{
    //    //_appFactory = appFactory;
    //    //_dbContext = appFactory.GetDbContext<IdentityDbContext>()!;
    //    //_client = appFactory.CreateClient();
    //}

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    //public async Task DisposeAsync()
    //{
    //    await _appFactory.ClearDbTable<IdentityDbContext>(IdentityDbContext.TableUsers);
    //    await _appFactory.ClearDbTable<IdentityDbContext>(IdentityDbContext.TableUserRefreshTokens);
    //}

    //[Fact]
    //public async Task Login_ReturnsValidationFailures_WhenRequestPayloadIsIncomplete()
    //{
    //    // Arrange

    //    // Act
    //    var uri = UriExtensions.MakeUriWithCookiesQueryString("/api/v1/auth/token", true);
    //    var payload = new AuthenticateRequest { };
    //    var response = await _client.PostAsJsonAsync(uri, payload);
    //    var content = await response.Content.ReadAsStringAsync();

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    content.Should().Contain("'Email' must not be empty.");
    //    content.Should().Contain("'Password' must not be empty.");
    //}

    //[Fact]
    //public async Task Login_ReturnsLockedResponse_WhenTooManyLoginFailures()
    //{
    //    // Arrange
    //    var userName = "test@test.com";
    //    var password = "Test1234";
    //    var passwordHasher = _appFactory.GetService<IPasswordHasher<IdentityAppUser>>()!;
    //    var identityUser = IdentityMocks.MakeIdentityAppUser(userName, password, passwordHasher);

    //    _dbContext.Users.Add(identityUser);
    //    await _dbContext.SaveChangesAsync();

    //    // MaxFailedAccessAttempts was set to "2" in IdentityTestFactory

    //    // Act
    //    var uri = UriExtensions.MakeUriWithCookiesQueryString("/api/v1/auth/token", true);
    //    var invalidCredentialsPayload = new AuthenticateRequest { Email = userName, Password = "WrongPassword" };
    //    await _client.PostAsJsonAsync(uri, invalidCredentialsPayload);
    //    var response = await _client.PostAsJsonAsync(uri, invalidCredentialsPayload);
    //    var content = await response.Content.ReadAsStringAsync();

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.Locked);
    //    content.Should().Contain("Account locked");
    //}

    //[Fact]
    //public async Task Login_ReturnsEmailNotConfirmedResponse_WhenIdentityNotConfirmed()
    //{
    //    // Arrange
    //    var userName = "test@test.com";
    //    var password = "Test1234";
    //    var passwordHasher = _appFactory.GetService<IPasswordHasher<IdentityAppUser>>()!;
    //    var identityUser = IdentityMocks.MakeIdentityAppUser(userName, password, passwordHasher);
    //    identityUser.EmailConfirmed = false;

    //    _dbContext.Users.Add(identityUser);
    //    await _dbContext.SaveChangesAsync();

    //    // Act
    //    var uri = UriExtensions.MakeUriWithCookiesQueryString("/api/v1/auth/token", true);
    //    var payload = new AuthenticateRequest { Email = userName, Password = password };
    //    var response = await _client.PostAsJsonAsync(uri, payload);
    //    var content = await response.Content.ReadAsStringAsync();

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.PreconditionRequired);
    //    content.Should().Contain("Email not confirmed");
    //}

    //[Theory]
    //[InlineData("wrong@user.com", "Test1234")]
    //[InlineData("test@test.com", "WrongPassword")]
    //public async Task Login_ReturnsUnauthorizedResponse_WhenIdentityNotFoundOrInvalidPassword(string payloadEmail, string payloadPassword)
    //{
    //    // Arrange
    //    var userName = "test@test.com";
    //    var password = "Test1234";
    //    var passwordHasher = _appFactory.GetService<IPasswordHasher<IdentityAppUser>>()!;
    //    var identityUser = IdentityMocks.MakeIdentityAppUser(userName, password, passwordHasher);

    //    _dbContext.Users.Add(identityUser);
    //    await _dbContext.SaveChangesAsync();

    //    // Act
    //    var uri = UriExtensions.MakeUriWithCookiesQueryString("/api/v1/auth/token", true);
    //    var payload = new AuthenticateRequest { Email = payloadEmail, Password = payloadPassword };
    //    var response = await _client.PostAsJsonAsync(uri, payload);
    //    var content = await response.Content.ReadAsStringAsync();

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    //    content.Should().Contain("Invalid credentials");
    //}

    //[Fact]
    //public async Task Login_ReturnsTokenResponse_WhenRequestPayloadIsValid()
    //{
    //    // Arrange
    //    var userName = "test@test.com";
    //    var password = "Test1234";
    //    var passwordHasher = _appFactory.GetService<IPasswordHasher<IdentityAppUser>>()!;
    //    var identityUser = IdentityMocks.MakeIdentityAppUser(userName, password, passwordHasher);

    //    _dbContext.Users.Add(identityUser);
    //    await _dbContext.SaveChangesAsync();

    //    // Act
    //    var uri = UriExtensions.MakeUriWithCookiesQueryString("/api/v1/auth/token", true);
    //    var payload = new AuthenticateRequest { Email = userName, Password = password };
    //    var response = await _client.PostAsJsonAsync(uri, payload);

    //    // Assert response
    //    response.StatusCode.Should().Be(HttpStatusCode.OK);
    //    var result = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
    //    result.Should().NotBeNull();
    //    result!.Token.Should().NotBeEmpty();
    //    result!.RefreshToken.Should().NotBeEmpty();

    //    // Assert db
    //    var userRefreshToken = await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == identityUser.Id);
    //    userRefreshToken.Should().NotBeNull();
    //    userRefreshToken!.Token.Should().Be(result.RefreshToken);
    //}

}
