using SmartPOSX.Domain.Base;
using SmartPOSX.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSX.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid? CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal DiscountAmount { get; set; }
        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal FinalAmount { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Refund> Refunds { get; set; }
        public virtual ICollection<LoyaltyTransaction> LoyaltyTransactions { get; set; }
    }
}
