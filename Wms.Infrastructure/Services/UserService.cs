using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Models.AuthModels;

namespace Wms.Infrastructure.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<User?> GetUserByToken(string token);
        Task<User?> CreateOrUpdateUser(User user);
        Task DeleteUser(string id);

        Task<IdentityResult> ValidatePasswordAsync(string password);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly WmsDbContext _dbContext;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, WmsDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        #region dbContext methods might be removed in the future
        public async Task<List<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User?> GetUserByToken(string token)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == token);
        }


        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to delete user: " + result.Errors.First().Description);
                }
            }
        }
        #endregion


        public async Task<User?> CreateOrUpdateUser(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);

            if (existingUser == null)
            {
                user.CreatedAt = DateTime.Now;
                var result = await _userManager.CreateAsync(user, user.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user: " + result.Errors.First().Description);
                }
                else
                {
                    string roleName = Enum.GetName((Enums.RoleType) user.Role);
                    await _userManager.AddToRoleAsync(user, roleName);
                    existingUser = user;
                }
            }
            else
            {
                // Update other fields as needed...
                existingUser.Email = user.Email;
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.PhoneNumber = user.PhoneNumber;

                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to update user: " + result.Errors.First().Description);
                }
                else
                {
                    var roles = await _userManager.GetRolesAsync(existingUser);
                    await _userManager.RemoveFromRolesAsync(existingUser, roles);

                    string roleName = Enum.GetName((Enums.RoleType)existingUser.Role);
                    await _userManager.AddToRoleAsync(existingUser, roleName);
                }
            }

            return existingUser;
        }

        public async Task<User> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByNameAsync(userName);
            return identityAppUser;
        }

        public async Task<User> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByIdAsync(userId);
            return identityAppUser;
        }

        public async Task<OperationResult> ConfirmEmailAsync(string userName, string code, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByEmailAsync(userName);
            var confirmEmailResult = await _userManager.ConfirmEmailAsync(identityAppUser, code);
            return OperationResult(confirmEmailResult);
        }

        public async Task<OperationResult> DeleteAsync(string userName, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByEmailAsync(userName);

            if (identityAppUser is null)
            {
                throw new Exception($"User not found for username: {userName}");
            }

            using (var dbContextTransaction = _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                
                var deleteIdentityResult = await _userManager.DeleteAsync(identityAppUser);

                // also check if its needed to delete AspNetUserTokens
                if (!deleteIdentityResult.Succeeded)
                {
                    await dbContextTransaction.Result.RollbackAsync(cancellationToken);
                }
                else
                {
                    await dbContextTransaction.Result.CommitAsync(cancellationToken);
                }

                return OperationResult(deleteIdentityResult);
            }
        }


        public async Task<OperationResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByIdAsync(user.Id);

            identityAppUser.Email = user.Email;
            identityAppUser.UserName = user.UserName;
            identityAppUser.FirstName = user.FirstName;
            identityAppUser.LastName = user.LastName;

            var createResult = await _userManager.UpdateAsync(identityAppUser);
            return OperationResult(createResult);
        }

        public async Task<OperationResult> ChangePasswordAsync(string userName, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByEmailAsync(userName);
            var changePasswordResult = await _userManager.ChangePasswordAsync(identityAppUser, currentPassword, newPassword);
            return OperationResult(changePasswordResult);
        }

        public async Task<OperationResult> ResetPasswordAsync(string userName, string code, string newPassword, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByEmailAsync(userName);
            var resetPasswordResult = await _userManager.ResetPasswordAsync(identityAppUser, code, newPassword);
            return OperationResult(resetPasswordResult);
        }


        public async Task<string> GeneratePasswordResetTokenAsync(string userName, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByEmailAsync(userName);
            return await _userManager.GeneratePasswordResetTokenAsync(identityAppUser);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userName, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByEmailAsync(userName);
            return await _userManager.GenerateEmailConfirmationTokenAsync(identityAppUser);
        }



        public async Task<bool> IsLockedOutAsync(string userName, CancellationToken cancellationToken = default)
        {
            var identityAppUser = await _userManager.FindByEmailAsync(userName);
            return await _userManager.IsLockedOutAsync(identityAppUser);
        }

        public async Task<IdentityResult> ValidatePasswordAsync(string password)
        {
            var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();
            if (passwordValidator == null)
                throw new InvalidOperationException("No password validator is configured.");

            var user = new User(); // Create an instance of User or use an existing user as needed

            return await passwordValidator.ValidateAsync(_userManager, user, password);
        }


        private static OperationResult OperationResult(IdentityResult identityResult)
        {
            var result = new OperationResult
            {
                Success = identityResult.Succeeded
            };

            if (identityResult.Succeeded is false)
            {
                var errors = identityResult.Errors.Select(x => x.Description).ToList();
                result.Description = string.Join("; ", errors);
            }

            return result;
        }

    }


}
