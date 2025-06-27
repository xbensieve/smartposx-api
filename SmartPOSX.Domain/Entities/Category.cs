using SmartPOSX.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace SmartPOSX.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
