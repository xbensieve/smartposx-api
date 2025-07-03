using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Products
{
    public class CreateVariationAttributeDto
    {
        [Required(ErrorMessage = "Attribute name is required.")]
        [MaxLength(50, ErrorMessage = "Attribute name cannot exceed 50 characters.")]
        [MinLength(2, ErrorMessage = "Attribute name must be at least 2 characters.")]
        public string AttributeName { get; set; }

        [Required(ErrorMessage = "Attribute value is required.")]
        [MaxLength(50, ErrorMessage = "Attribute value cannot exceed 50 characters.")]
        [MinLength(1, ErrorMessage = "Attribute value must be at least 1 character.")]
        public string AttributeValue { get; set; }
    }
}
