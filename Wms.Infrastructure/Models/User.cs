using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Wms.Infrastructure.Models.AuthModels;
using static Wms.Infrastructure.Enums;

namespace Wms.Infrastructure.Models;

public partial class User: IdentityUser
{
    [ValidatePassword]
    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [Range(0, 2, ErrorMessage = "Role must be between 0 and 2")]
    public RoleType Role { get; set; }

    public string? RefreshToken { get; internal set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<OrderTrace> OrderTraces { get; set; } = new List<OrderTrace>();
}


public partial class UserDto
{
    //public string Id { get; set; }

    [Required]
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    [Range(0, 2, ErrorMessage = "Role must be between 0 and 2")]
    public RoleType Role { get; set; }

}
