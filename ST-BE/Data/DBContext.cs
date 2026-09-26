using Microsoft.EntityFrameworkCore;
using ST_BE.Models;

namespace ST_BE.Data
{
    public class DBContext : DbContext
    {
            public DBContext(DbContextOptions<DBContext> options) : base(options) { }

            // Khai báo các bảng dữ liệu ánh xạ từ Model
            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }

            // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Nạp sẵn 5 danh mục ban đầu vào SQL Server ngay khi tạo bảng
                modelBuilder.Entity<Category>().HasData(
                    new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo dẻo" },
                    new Category { CategoryId = 2, CategoryName = "Nước giải khát & Trà", Description = "Nước ngọt, nước khoáng, trà" },
                    new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi, sữa chua, phô mai" },
                    new Category { CategoryId = 4, CategoryName = "Mì gói & Thực phẩm ăn liền", Description = "Mì ăn liền, phở khô, cháo gói" },
                    new Category { CategoryId = 5, CategoryName = "Gia vị & Dầu ăn", Description = "Nước mắm, hạt nêm, dầu thực vật" }
                );
            }
        }
    }
