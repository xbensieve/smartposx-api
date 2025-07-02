using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Products
{
    public class CreateVariationAttributeDto
    {
        [Required]
        [MaxLength(50)]
        public string AttributeName { get; set; }
        [Required]
        [MaxLength(50)]
        public string AttributeValue { get; set; }
    }
}
