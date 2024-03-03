using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Models.AuthModels;
using Wms.Infrastructure.Services;
using Wms.Infrastructure.Services.AuthServices;

namespace Wms.Api.Controllers.Areas.integration
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    //[Obsolete("Not implemented yet")]
    [Area("integration")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;


        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// - operational.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var operationResult = await _tokenService.Authenticate(loginRequest.userName, loginRequest.password);

            if (!operationResult.Success)
            {
                return Unauthorized(operationResult.Message);
            }

            return Ok(new { operationResult.AccessToken, operationResult.RefreshToken });
        }


        /// <summary>
        /// - implemented, not tested.
        /// </summary>
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            // Find the user associated with this refresh token
            var user = await _userService.GetUserByToken(refreshTokenRequest.RefreshToken);

            if (user == null)
            {
                return BadRequest("Invalid refresh token.");
            }

            // Generate a new access token and refresh token for this user
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = await _tokenService.GenerateRefreshToken(user);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        /// <summary>
        /// - implemented, not tested.
        /// </summary>
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword(object resetPasswordDto)
        {
            // This would typically involve checking if the user exists,
            // checking if the provided token is valid,
            // and then resetting the password.

            //_userService.ResetPassword(resetPasswordDto);
            return Ok();
        }

        /// <summary>
        /// - implemented, not tested.
        /// </summary>
        [HttpPost("activateaccount")]
        public IActionResult ActivateAccount(object activateAccountDto)
        {
            // This would typically involve checking if the user exists,
            // checking if the provided token is valid,
            // and then activating the account.

            //_userService.ActivateAccount(activateAccountDto);
            return Ok();
        }

        /// <summary>
        /// - implemented, not tested.
        /// </summary>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // This would typically involve invalidating the current user's token.
            _tokenService.InvalidateCurrentUserToken();
            return Ok();
        }

    }
}
