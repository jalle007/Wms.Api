using System.ComponentModel.DataAnnotations;

namespace Wms.Infrastructure.Models.AuthModels
{
    public class LoginRequest
    {
        [Required]
        public string userName { get; set; }
        
        [Required]
        public  string password { get; set; }
    }
}
