using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Models.AuthModels;

namespace Wms.Infrastructure.Services.AuthServices
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(User user);
        void ValidateAccessToken(string token);
        void InvalidateCurrentUserToken();
        string RefreshToken(string refreshToken);
        Task<OperationResult> Authenticate(string username, string password);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;


        public TokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _config = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(_config["Jwt:Key"]);
            //var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);


            // Get user roles
            var userRoles = await _userManager.GetRolesAsync(user);

            // Create a list to hold the claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Id.ToString()),
        // Include more claims as needed
    };

            // Add a claim for each role
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<string> GenerateRefreshToken(User user)
        {
            var refreshToken = Guid.NewGuid().ToString();

            // Store the refresh token in the user's account in the database
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            return refreshToken;
        }

        public void ValidateAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Set clock skew to zero so tokens expire exactly at token expiration time
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
        }

        public void InvalidateCurrentUserToken()
        {
            // Get the current user's refresh token from the database
            // var refreshToken = _context.RefreshTokens.SingleOrDefault(rt => rt.Username == User.Identity.Name);

            // if (refreshToken != null)
            // {
            //     // Invalidate the refresh token
            //     _context.RefreshTokens.Remove(refreshToken);
            //     await _context.SaveChangesAsync();
            // }
        }

        public string RefreshToken(string refreshToken)
        {
            // Get the refresh token from the database
            // var storedRefreshToken = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);

            // if (storedRefreshToken == null || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
            // {
            //     throw new SecurityTokenException("Invalid refresh token");
            // }

            // Get the user from the database
            // var user = _context.Users.SingleOrDefault(u => u.Username == storedRefreshToken.Username);

            // Invalidate the refresh token
            // _context.RefreshTokens.Remove(storedRefreshToken);

            // Generate a new refresh token
            // var newRefreshToken = GenerateRefreshToken(user);
            // _context.RefreshTokens.Add(newRefreshToken);

            // Save changes in the database
            // await _context.SaveChangesAsync();

            // Generate a new access token
            // var accessToken = GenerateAccessToken(user);

            // return new AuthenticationResponse
            // {
            //     AccessToken = accessToken,
            //     RefreshToken = newRefreshToken
            // };

            return "";
        }

        public async Task<OperationResult> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return new OperationResult { Success = false, Message = "Username or password is incorrect" };
            }

            var accessToken = await GenerateAccessToken(user);
            var refreshToken = await GenerateRefreshToken(user);

            return new OperationResult { Success = true, Message = "Authentication successful", AccessToken =  accessToken, RefreshToken = refreshToken };
        }


    }

}
