using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Employees
{
    public class CreateEmployeeDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        [Required]
        public Guid RoleId { get; set; }
    }
}
