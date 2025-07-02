using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Products
{
    public class CreateProductVariationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative integer.")]
        public int Stock { get; set; }
        public List<CreateVariationAttributeDto> VariationAttributes { get; set; }
        public List<CreateVariationImageDto> VariationImages { get; set; }
    }
}
