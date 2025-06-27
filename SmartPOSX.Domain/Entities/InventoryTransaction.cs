using SmartPOSX.Domain.Base;
using SmartPOSX.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSX.Domain.Entities
{
    public class InventoryTransaction : BaseEntity
    {
        public Guid ProductVariationId { get; set; }
        public int Quantity { get; set; }
        [Required]
        public InventoryTransactionType InventoryTransactionType { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }
        [ForeignKey(nameof(ProductVariationId))]
        public virtual ProductVariation ProductVariation { get; set; }
    }
}
