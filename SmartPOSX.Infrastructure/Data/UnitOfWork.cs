using Microsoft.EntityFrameworkCore.Storage;
using SmartPOSX.Core.Interfaces;
using SmartPOSX.Core.Interfaces.Repositories;
using SmartPOSX.Domain.Entities;
using SmartPOSX.Infrastructure.Repositories;

namespace SmartPOSX.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;
        public IGenericRepository<Employee> Employees { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Supplier> Suppliers { get; }
        public IGenericRepository<Role> Roles { get; }
        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<ProductVariation> ProductVariations { get; }
        public IGenericRepository<VariationAttribute> VariationAttributes { get; }
        public IGenericRepository<VariationImage> VariationImages { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<OrderItem> OrderItems { get; }
        public IGenericRepository<Refund> Refunds { get; }
        public IGenericRepository<InventoryTransaction> InventoryTransactions { get; }
        public IGenericRepository<PurchaseOrder> PurchaseOrders { get; }
        public IGenericRepository<PurchaseOrderItem> PurchaseOrderItems { get; }
        public IGenericRepository<LoyaltyTransaction> LoyaltyTransactions { get; }
        public IGenericRepository<EmployeeLog> EmployeeLogs { get; }
        public IGenericRepository<Payment> Payments { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Employees = new GenericRepository<Employee>(_context);
            Categories = new GenericRepository<Category>(_context);
            Suppliers = new GenericRepository<Supplier>(_context);
            Roles = new GenericRepository<Role>(_context);
            Customers = new GenericRepository<Customer>(_context);
            Products = new GenericRepository<Product>(_context);
            ProductVariations = new GenericRepository<ProductVariation>(_context);
            VariationAttributes = new GenericRepository<VariationAttribute>(_context);
            VariationImages = new GenericRepository<VariationImage>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderItems = new GenericRepository<OrderItem>(_context);
            Refunds = new GenericRepository<Refund>(_context);
            InventoryTransactions = new GenericRepository<InventoryTransaction>(_context);
            PurchaseOrders = new GenericRepository<PurchaseOrder>(_context);
            PurchaseOrderItems = new GenericRepository<PurchaseOrderItem>(_context);
            LoyaltyTransactions = new GenericRepository<LoyaltyTransaction>(_context);
            EmployeeLogs = new GenericRepository<EmployeeLog>(_context);
            Payments = new GenericRepository<Payment>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
