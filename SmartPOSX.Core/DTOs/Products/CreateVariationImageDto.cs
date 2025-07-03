using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Core.DTOs.Products
{
    public class CreateVariationImageDto
    {
        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Image URL must be a valid URL.")]
        [MaxLength(500, ErrorMessage = "Image URL cannot exceed 500 characters.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Public ID is required.")]
        [MaxLength(200, ErrorMessage = "Public ID cannot exceed 200 characters.")]
        public string PublicId { get; set; }
    }
}
