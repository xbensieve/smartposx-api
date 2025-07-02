using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Products
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public decimal BasePrice { get; set; }
        public Guid CategoryId { get; set; }
        public List<CreateProductVariationDto> ProductVariations { get; set; }
    }
}
