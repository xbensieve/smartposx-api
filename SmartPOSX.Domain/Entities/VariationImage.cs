using SmartPOSX.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSX.Domain.Entities
{
    public class VariationImage : BaseEntity
    {
        public Guid ProductVariationId { get; set; }
        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(100)]
        public string FileName { get; set; }
        [MaxLength(200)]
        public string AltText { get; set; }
        public bool IsPrimary { get; set; }
        public int? FileSize { get; set; }
        [ForeignKey(nameof(ProductVariationId))]
        public virtual ProductVariation ProductVariation { get; set; }
    }
}
