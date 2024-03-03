using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Infrastructure.Services;

namespace Wms.Infrastructure.Models.AuthModels
{
    public class ValidatePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userService = (IUserService)validationContext.GetService(typeof(IUserService));
            var password = value as string;

            var result = userService.ValidatePasswordAsync(password).Result;

            return result.Succeeded ? ValidationResult.Success : new ValidationResult("Invalid password.");
        }
    }

}
