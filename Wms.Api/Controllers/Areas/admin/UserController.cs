using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;

namespace Wms.Api.Controllers.Areas.admin
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/v1/user
        /// <summary>
        /// - operational.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        // GET: api/v1/user/{id}
        /// <summary>
        /// - operational.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/user
        /// <summary>
        /// - operational.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateUser([FromBody] UserDto userDto)
        {
            try
            {
                // Validate the password using the UserService's ValidatePasswordAsync method
                var passwordValidationResult = await _userService.ValidatePasswordAsync(userDto.Password);
                if (!passwordValidationResult.Succeeded)
                {
                    var passwordErrorMessage = string.Join("; ", passwordValidationResult.Errors.Select(e => e.Description));
                    return BadRequest(new { Error = $"Password validation failed: {passwordErrorMessage}" });
                }

                // Create a User object from the provided UserDto
                var user = new User
                {
                    UserName = userDto.UserName,
                    Password = userDto.Password,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    Role = userDto.Role
                };

                // Call the UserService's CreateOrUpdateUser method
                var createdOrUpdatedUser = await _userService.CreateOrUpdateUser(user);

                if (createdOrUpdatedUser != null)
                {
                    return Ok(createdOrUpdatedUser); // Return the created or updated user
                }
                else
                {
                    return BadRequest(new { Error = "Failed to create or update user." }); // Return an appropriate error message
                }
            }
            catch (Exception ex)
            {
                // Return a generic error message
                return StatusCode(500, new { Error = $"An unexpected error occurred: {ex.Message}" });
            }
        }


        // DELETE: api/v1/user/{id}
        /// <summary>
        /// - implemented, not tested.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        // GET: api/v1/profile
        /// <summary>
        /// Gets the profile of the user.
        /// </summary>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _userService.GetAllUsers();
            var user = result.FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


    }
}