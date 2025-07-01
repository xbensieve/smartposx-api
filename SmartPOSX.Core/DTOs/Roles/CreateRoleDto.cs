using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Roles
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [MinLength(1, ErrorMessage = "At least one permission must be specified.")]
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
