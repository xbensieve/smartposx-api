using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Products
{
    public class CreateProductVariationDto
    {
        [Required(ErrorMessage = "Variation name is required.")]
        [MaxLength(100, ErrorMessage = "Variation name cannot exceed 100 characters.")]
        [MinLength(2, ErrorMessage = "Variation name must be at least 2 characters.")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative integer.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Variation attributes are required.")]
        [MinLength(1, ErrorMessage = "At least one variation attribute is required.")]
        public List<CreateVariationAttributeDto> VariationAttributes { get; set; }

        [Required(ErrorMessage = "Variation images are required.")]
        [MinLength(1, ErrorMessage = "At least one variation image is required.")]
        public List<CreateVariationImageDto> VariationImages { get; set; }
    }
}
