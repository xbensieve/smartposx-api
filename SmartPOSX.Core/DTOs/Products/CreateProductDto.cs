using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Products
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        [MinLength(2, ErrorMessage = "Product name must be at least 2 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Base price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base price must be greater than 0.")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "At least one product variation is required.")]
        [MinLength(1, ErrorMessage = "There must be at least one product variation.")]
        public List<CreateProductVariationDto> ProductVariations { get; set; }
    }
}
