using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
