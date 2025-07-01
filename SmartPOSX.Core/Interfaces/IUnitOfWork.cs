using SmartPOSX.Core.Interfaces.Repositories;
using SmartPOSX.Domain.Entities;

namespace SmartPOSX.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<Role> Roles { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Supplier> Suppliers { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<ProductVariation> ProductVariations { get; }
        IGenericRepository<VariationAttribute> VariationAttributes { get; }
        IGenericRepository<VariationImage> VariationImages { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IGenericRepository<Refund> Refunds { get; }
        IGenericRepository<InventoryTransaction> InventoryTransactions { get; }
        IGenericRepository<PurchaseOrder> PurchaseOrders { get; }
        IGenericRepository<PurchaseOrderItem> PurchaseOrderItems { get; }
        IGenericRepository<LoyaltyTransaction> LoyaltyTransactions { get; }
        IGenericRepository<EmployeeLog> EmployeeLogs { get; }
        IGenericRepository<Payment> Payments { get; }
        Task<int> SaveChangesAsync();
    }
}
