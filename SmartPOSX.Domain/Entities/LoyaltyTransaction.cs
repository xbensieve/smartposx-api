using SmartPOSX.Domain.Base;
using SmartPOSX.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSX.Domain.Entities
{
    public class LoyaltyTransaction : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid? OrderId { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public LoyaltyTransactionType LoyaltyTransactionType { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
