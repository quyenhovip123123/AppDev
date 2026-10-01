using ST_BE.Models;

namespace ST_BE.Data
{
    // Tạo dữ liệu mẫu cho Cửa hàng Văn phòng phẩm Sunny (chỉ chạy khi CSDL còn trống)
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Users.Any()) return;

            db.Users.AddRange(
                new User { Username = "admin", FullName = "Nguyễn Văn Quản Lý", Role = Roles.Admin, PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") },
                new User { Username = "nhanvien", FullName = "Trần Thị Bán Hàng", Role = Roles.Staff, PasswordHash = BCrypt.Net.BCrypt.HashPassword("nhanvien123") }
            );

            var but = new Category { CategoryName = "Bút viết", Description = "Bút bi, bút gel, bút chì, bút dạ quang" };
            var vo = new Category { CategoryName = "Vở & Sổ", Description = "Vở học sinh, sổ tay, sổ lò xo" };
            var giay = new Category { CategoryName = "Giấy in & Giấy note", Description = "Giấy A4, A5, giấy note, giấy màu" };
            var hocTap = new Category { CategoryName = "Dụng cụ học tập", Description = "Thước, gôm, gọt chì, compa, hộp bút" };
            var vanPhong = new Category { CategoryName = "Dụng cụ văn phòng", Description = "Bấm kim, kẹp giấy, bìa hồ sơ, băng keo" };
            var myThuat = new Category { CategoryName = "Mỹ thuật", Description = "Bút màu, màu nước, giấy vẽ, cọ vẽ" };
            db.Categories.AddRange(but, vo, giay, hocTap, vanPhong, myThuat);

            db.Products.AddRange(
                P("BUT001", "Bút bi Thiên Long TL-027 xanh", but, "Cây", 3500, 5000, 300),
                P("BUT002", "Bút bi Thiên Long TL-027 đỏ", but, "Cây", 3500, 5000, 150),
                P("BUT003", "Bút gel Thiên Long GEL-012", but, "Cây", 5500, 8000, 120),
                P("BUT004", "Bút chì gỗ 2B", but, "Cây", 2500, 4000, 200),
                P("BUT005", "Bút dạ quang vàng", but, "Cây", 6000, 9000, 8),
                P("VO001", "Vở kẻ ngang 96 trang", vo, "Quyển", 7000, 10000, 250),
                P("VO002", "Vở kẻ ngang 200 trang", vo, "Quyển", 14000, 19000, 180),
                P("VO003", "Sổ lò xo A5 100 trang", vo, "Quyển", 18000, 25000, 60),
                P("GIAY001", "Giấy in A4 70gsm", giay, "Ram", 62000, 75000, 40),
                P("GIAY002", "Giấy in A4 80gsm", giay, "Ram", 75000, 89000, 25),
                P("GIAY003", "Giấy note 3x3 vàng", giay, "Xấp", 7000, 12000, 90),
                P("HT001", "Thước kẻ nhựa 20cm", hocTap, "Cây", 3000, 5000, 100),
                P("HT002", "Gôm tẩy trắng", hocTap, "Cục", 2000, 4000, 160),
                P("HT003", "Gọt bút chì", hocTap, "Cái", 4000, 7000, 5),
                P("HT004", "Compa học sinh", hocTap, "Cái", 15000, 22000, 35),
                P("VP001", "Bấm kim số 10", vanPhong, "Cái", 25000, 35000, 30),
                P("VP002", "Kẹp giấy 32mm (hộp)", vanPhong, "Hộp", 8000, 12000, 70),
                P("VP003", "Bìa hồ sơ nút bấm A4", vanPhong, "Cái", 3000, 5000, 220),
                P("MT001", "Bút sáp màu 12 màu", myThuat, "Hộp", 18000, 26000, 45),
                P("MT002", "Màu nước 12 màu", myThuat, "Hộp", 30000, 42000, 3)
            );

            db.Customers.AddRange(
                new Customer { FullName = "Lê Minh Anh", Phone = "0901234567", Email = "minhanh@example.com", Address = "Q.1, TP.HCM" },
                new Customer { FullName = "Phạm Thu Hà", Phone = "0912345678", Address = "Q.3, TP.HCM" },
                new Customer { FullName = "Công ty TNHH ABC", Phone = "02838123456", Email = "vanphong@abc.example", Address = "Q.7, TP.HCM" }
            );

            db.SaveChanges();
        }

        private static Product P(string code, string name, Category cat, string unit, decimal cost, decimal price, int stock) =>
            new() { ProductCode = code, ProductName = name, Category = cat, Unit = unit, CostPrice = cost, SalePrice = price, StockQuantity = stock };
    }
}
