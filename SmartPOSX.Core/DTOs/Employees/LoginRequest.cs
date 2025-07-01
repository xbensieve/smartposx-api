using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Employees
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
