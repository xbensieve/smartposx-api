using SmartPOSX.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSX.Domain.Entities
{
    public class PurchaseOrderItem : BaseEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductVariationId { get; set; }
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal UnitPrice { get; set; }
        [ForeignKey(nameof(PurchaseOrderId))]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [ForeignKey(nameof(ProductVariationId))]
        public virtual ProductVariation ProductVariation { get; set; }
    }
}
