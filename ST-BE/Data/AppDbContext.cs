using Microsoft.EntityFrameworkCore;
using ST_BE.Models;

namespace ST_BE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<ImportReceipt> ImportReceipts => Set<ImportReceipt>();
        public DbSet<ImportReceiptDetail> ImportReceiptDetails => Set<ImportReceiptDetail>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Tiền tệ: decimal(18,2)
            foreach (var prop in mb.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal)))
            {
                prop.SetPrecision(18);
                prop.SetScale(2);
            }

            mb.Entity<User>(e =>
            {
                e.HasIndex(u => u.Username).IsUnique();
                e.Property(u => u.Username).HasMaxLength(50).IsRequired();
                e.Property(u => u.FullName).HasMaxLength(100).IsRequired();
                e.Property(u => u.Role).HasMaxLength(20).IsRequired();
                e.Property(u => u.SecurityStamp).HasMaxLength(50);
            });

            mb.Entity<RefreshToken>(e =>
            {
                e.HasIndex(t => t.TokenHash).IsUnique();
                e.Property(t => t.TokenHash).HasMaxLength(100).IsRequired();
                e.Ignore(t => t.IsActive);
                e.HasOne(t => t.User).WithMany(u => u.RefreshTokens)
                    .HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<Category>(e =>
            {
                e.Property(c => c.CategoryName).HasMaxLength(100).IsRequired();
                e.Property(c => c.Description).HasMaxLength(255);
            });

            mb.Entity<Product>(e =>
            {
                e.HasIndex(p => p.ProductCode).IsUnique();
                e.Property(p => p.ProductCode).HasMaxLength(30).IsRequired();
                // Mã vạch không bắt buộc nhưng đã có thì không được trùng (bỏ qua các dòng NULL)
                e.HasIndex(p => p.Barcode).IsUnique().HasFilter("[Barcode] IS NOT NULL");
                e.Property(p => p.Barcode).HasMaxLength(20);
                e.Property(p => p.ProductName).HasMaxLength(150).IsRequired();
                e.Property(p => p.Unit).HasMaxLength(20);
                e.Property(p => p.Description).HasMaxLength(255);
                e.HasOne(p => p.Category).WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
                // Ràng buộc kiểm tra ngay trong CSDL: giá bán > 0, tồn kho không âm
                e.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Products_SalePrice", "[SalePrice] > 0");
                    t.HasCheckConstraint("CK_Products_CostPrice", "[CostPrice] >= 0");
                    t.HasCheckConstraint("CK_Products_StockQuantity", "[StockQuantity] >= 0");
                });
            });

            mb.Entity<Customer>(e =>
            {
                e.HasIndex(c => c.Phone).IsUnique();
                e.Property(c => c.FullName).HasMaxLength(100).IsRequired();
                e.Property(c => c.Phone).HasMaxLength(15).IsRequired();
                e.Property(c => c.Email).HasMaxLength(100);
                e.Property(c => c.Address).HasMaxLength(255);
                e.ToTable(t => t.HasCheckConstraint("CK_Customers_Points", "[Points] >= 0"));
            });

            mb.Entity<Order>(e =>
            {
                e.HasIndex(o => o.OrderCode).IsUnique();
                e.Property(o => o.OrderCode).HasMaxLength(30).IsRequired();
                e.Property(o => o.Status).HasMaxLength(20);
                e.Property(o => o.Note).HasMaxLength(255);
                e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Restrict);
                e.HasIndex(o => o.CreatedAt);
            });

            mb.Entity<OrderDetail>(e =>
            {
                e.Property(d => d.ProductName).HasMaxLength(150);
                e.ToTable(t => t.HasCheckConstraint("CK_OrderDetails_Quantity", "[Quantity] > 0"));
                e.HasOne(d => d.Order).WithMany(o => o.Details).HasForeignKey(d => d.OrderId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(d => d.Product).WithMany().HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<ImportReceipt>(e =>
            {
                e.HasIndex(r => r.ReceiptCode).IsUnique();
                e.Property(r => r.ReceiptCode).HasMaxLength(30).IsRequired();
                e.Property(r => r.SupplierName).HasMaxLength(150).IsRequired();
                e.Property(r => r.Note).HasMaxLength(255);
                e.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<ImportReceiptDetail>(e =>
            {
                e.HasOne(d => d.ImportReceipt).WithMany(r => r.Details).HasForeignKey(d => d.ImportReceiptId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(d => d.Product).WithMany().HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
