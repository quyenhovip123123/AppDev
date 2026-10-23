using Microsoft.EntityFrameworkCore;
using ST_BE.Models;

namespace ST_BE.Data
{
    // Tạo dữ liệu mẫu cho Cửa hàng Văn phòng phẩm Sunny.
    // Chạy mỗi lần khởi động nhưng chỉ THÊM những gì còn thiếu (so theo username / tên nhóm / mã hàng / SĐT),
    // nên dùng được cho cả CSDL mới lẫn CSDL đã có dữ liệu — không ghi đè dữ liệu người dùng đã nhập.
    public static class DbSeeder
    {
        private const string DefaultStaffPassword = "nhanvien123";

        public static void Seed(AppDbContext db)
        {
            SeedUsers(db);
            SeedCatalog(db);
            SeedCustomers(db);
            SeedHistory(db);
        }

        // ===== Tài khoản =====
        private static void SeedUsers(AppDbContext db)
        {
            var users = new[]
            {
                new User { Username = "admin", FullName = "Nguyễn Văn Quản Lý", Role = Roles.Admin, PasswordHash = "admin123" },
                new User { Username = "nhanvien", FullName = "Trần Thị Bán Hàng", Role = Roles.Staff, PasswordHash = DefaultStaffPassword },
                new User { Username = "tuanpm", FullName = "Phạm Minh Tuấn", Role = Roles.Staff, PasswordHash = DefaultStaffPassword },
                new User { Username = "landt", FullName = "Đỗ Thị Lan", Role = Roles.Staff, PasswordHash = DefaultStaffPassword },
                new User { Username = "baohg", FullName = "Hoàng Gia Bảo", Role = Roles.Staff, PasswordHash = DefaultStaffPassword, IsActive = false },
                new User { Username = "kiemkho", FullName = "Lê Văn Kiểm Kho", Role = Roles.Warehouse, PasswordHash = DefaultStaffPassword },
            };

            var existing = db.Users.Select(u => u.Username).ToHashSet();
            foreach (var u in users.Where(u => !existing.Contains(u.Username)))
            {
                u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.PasswordHash);
                db.Users.Add(u);
            }
            db.SaveChanges();
        }

        // ===== Nhóm hàng + Mặt hàng =====
        private record ProductSeed(string Code, string Name, string Category, string Unit, decimal Cost, decimal Price, int Stock);

        private static readonly (string Name, string Description)[] Categories =
        {
            ("Bút viết", "Bút bi, bút gel, bút chì, bút dạ quang"),
            ("Vở & Sổ", "Vở học sinh, sổ tay, sổ lò xo"),
            ("Giấy in & Giấy note", "Giấy A4, A5, giấy note, giấy màu"),
            ("Dụng cụ học tập", "Thước, gôm, gọt chì, compa, hộp bút"),
            ("Dụng cụ văn phòng", "Bấm kim, kẹp giấy, bìa hồ sơ, băng keo"),
            ("Mỹ thuật", "Bút màu, màu nước, giấy vẽ, cọ vẽ"),
            ("Bìa & Lưu trữ hồ sơ", "Bìa còng, bìa lá, file tài liệu, kệ hồ sơ"),
            ("Băng keo & Keo dán", "Băng keo, keo sữa, hồ khô, súng bắn keo"),
            ("Máy tính cầm tay", "Máy tính khoa học, máy tính để bàn, pin"),
            ("Balo & Túi bút", "Balo học sinh, túi bút, túi hồ sơ"),
            ("Mực & Ruột bút", "Ruột bút, mực bút máy, ruột chì kim, mực dấu"),
            ("Quà tặng & Thiệp", "Thiệp chúc mừng, giấy gói quà, hộp quà"),
        };

        private static readonly ProductSeed[] Products =
        {
            new("BUT001", "Bút bi Thiên Long TL-027 xanh", "Bút viết", "Cây", 3500, 5000, 300),
            new("BUT002", "Bút bi Thiên Long TL-027 đỏ", "Bút viết", "Cây", 3500, 5000, 150),
            new("BUT003", "Bút gel Thiên Long GEL-012", "Bút viết", "Cây", 5500, 8000, 120),
            new("BUT004", "Bút chì gỗ 2B", "Bút viết", "Cây", 2500, 4000, 200),
            new("BUT005", "Bút dạ quang vàng", "Bút viết", "Cây", 6000, 9000, 8),
            new("BUT006", "Bút bi TL-079 (hộp 20 cây)", "Bút viết", "Hộp", 62000, 85000, 40),
            new("BUT007", "Bút lông bảng WB-03 xanh", "Bút viết", "Cây", 6500, 10000, 120),
            new("BUT008", "Bút chì kim 0.5mm", "Bút viết", "Cây", 12000, 18000, 75),
            new("BUT009", "Bút xóa nước 12ml", "Bút viết", "Cây", 14000, 20000, 60),
            new("BUT010", "Bút ký mực gel 0.7mm đen", "Bút viết", "Cây", 9000, 14000, 9),

            new("VO001", "Vở kẻ ngang 96 trang", "Vở & Sổ", "Quyển", 7000, 10000, 250),
            new("VO002", "Vở kẻ ngang 200 trang", "Vở & Sổ", "Quyển", 14000, 19000, 180),
            new("VO003", "Sổ lò xo A5 100 trang", "Vở & Sổ", "Quyển", 18000, 25000, 60),
            new("VO004", "Vở ô ly 4 ô 96 trang", "Vở & Sổ", "Quyển", 8000, 12000, 300),
            new("VO005", "Sổ bìa da A5 200 trang", "Vở & Sổ", "Quyển", 45000, 65000, 30),
            new("VO006", "Sổ tay mini A6", "Vở & Sổ", "Quyển", 9000, 15000, 140),
            new("VO007", "Vở 5 ô ly 80 trang (lốc 10)", "Vở & Sổ", "Lốc", 70000, 95000, 7),

            new("GIAY001", "Giấy in A4 70gsm", "Giấy in & Giấy note", "Ram", 62000, 75000, 40),
            new("GIAY002", "Giấy in A4 80gsm", "Giấy in & Giấy note", "Ram", 75000, 89000, 25),
            new("GIAY003", "Giấy note 3x3 vàng", "Giấy in & Giấy note", "Xấp", 7000, 12000, 90),
            new("GIAY004", "Giấy in A3 70gsm", "Giấy in & Giấy note", "Ram", 120000, 145000, 15),
            new("GIAY005", "Giấy note phân trang 5 màu", "Giấy in & Giấy note", "Xấp", 10000, 16000, 85),
            new("GIAY006", "Giấy màu thủ công A4 (100 tờ)", "Giấy in & Giấy note", "Xấp", 28000, 39000, 50),
            new("GIAY007", "Giấy than A4 (hộp 100 tờ)", "Giấy in & Giấy note", "Hộp", 85000, 110000, 6),

            new("HT001", "Thước kẻ nhựa 20cm", "Dụng cụ học tập", "Cây", 3000, 5000, 100),
            new("HT002", "Gôm tẩy trắng", "Dụng cụ học tập", "Cục", 2000, 4000, 160),
            new("HT003", "Gọt bút chì", "Dụng cụ học tập", "Cái", 4000, 7000, 5),
            new("HT004", "Compa học sinh", "Dụng cụ học tập", "Cái", 15000, 22000, 35),
            new("HT005", "Bộ thước Eke 4 món", "Dụng cụ học tập", "Bộ", 12000, 18000, 90),
            new("HT006", "Hộp bút nhựa 2 ngăn", "Dụng cụ học tập", "Cái", 25000, 38000, 45),
            new("HT007", "Bảng con học sinh 2 mặt", "Dụng cụ học tập", "Cái", 15000, 22000, 60),

            new("VP001", "Bấm kim số 10", "Dụng cụ văn phòng", "Cái", 25000, 35000, 30),
            new("VP002", "Kẹp giấy 32mm (hộp)", "Dụng cụ văn phòng", "Hộp", 8000, 12000, 70),
            new("VP003", "Bìa hồ sơ nút bấm A4", "Dụng cụ văn phòng", "Cái", 3000, 5000, 220),
            new("VP004", "Ghim bấm số 10 (hộp)", "Dụng cụ văn phòng", "Hộp", 3000, 5000, 400),
            new("VP005", "Kéo văn phòng 21cm", "Dụng cụ văn phòng", "Cái", 18000, 28000, 55),
            new("VP006", "Dao rọc giấy lớn", "Dụng cụ văn phòng", "Cái", 12000, 19000, 70),
            new("VP007", "Kẹp bướm 25mm (hộp 12 cái)", "Dụng cụ văn phòng", "Hộp", 14000, 21000, 4),

            new("MT001", "Bút sáp màu 12 màu", "Mỹ thuật", "Hộp", 18000, 26000, 45),
            new("MT002", "Màu nước 12 màu", "Mỹ thuật", "Hộp", 30000, 42000, 3),
            new("MT003", "Bút chì màu 24 màu", "Mỹ thuật", "Hộp", 35000, 49000, 40),
            new("MT004", "Giấy vẽ A3 (xấp 20 tờ)", "Mỹ thuật", "Xấp", 22000, 32000, 35),
            new("MT005", "Bộ cọ vẽ 6 cây", "Mỹ thuật", "Bộ", 28000, 42000, 25),
            new("MT006", "Đất nặn 12 màu", "Mỹ thuật", "Hộp", 24000, 35000, 14),

            new("LT001", "Bìa còng 7cm A4", "Bìa & Lưu trữ hồ sơ", "Cái", 38000, 52000, 60),
            new("LT002", "Bìa lá A4 (xấp 10 cái)", "Bìa & Lưu trữ hồ sơ", "Xấp", 15000, 22000, 120),
            new("LT003", "File 40 lá A4", "Bìa & Lưu trữ hồ sơ", "Cái", 32000, 45000, 45),
            new("LT004", "Hộp đựng tài liệu 10cm", "Bìa & Lưu trữ hồ sơ", "Cái", 22000, 32000, 38),
            new("LT005", "Kệ tài liệu 3 ngăn", "Bìa & Lưu trữ hồ sơ", "Cái", 65000, 89000, 12),

            new("KD001", "Băng keo trong 5cm", "Băng keo & Keo dán", "Cuộn", 12000, 18000, 150),
            new("KD002", "Băng keo 2 mặt 2.4cm", "Băng keo & Keo dán", "Cuộn", 8000, 13000, 110),
            new("KD003", "Keo dán giấy 30ml", "Băng keo & Keo dán", "Chai", 5000, 8000, 200),
            new("KD004", "Hồ khô dạng thỏi 8g", "Băng keo & Keo dán", "Thỏi", 6000, 10000, 160),
            new("KD005", "Súng bắn keo nến mini", "Băng keo & Keo dán", "Cái", 45000, 65000, 8),

            new("MTC001", "Máy tính Casio FX-580VN X", "Máy tính cầm tay", "Cái", 590000, 720000, 18),
            new("MTC002", "Máy tính Casio FX-880BTG", "Máy tính cầm tay", "Cái", 690000, 830000, 16),
            new("MTC003", "Máy tính để bàn 12 số", "Máy tính cầm tay", "Cái", 120000, 165000, 22),
            new("MTC004", "Pin AAA (vỉ 4 viên)", "Máy tính cầm tay", "Vỉ", 18000, 26000, 90),

            new("BL001", "Balo học sinh chống gù", "Balo & Túi bút", "Cái", 210000, 295000, 15),
            new("BL002", "Túi bút vải canvas", "Balo & Túi bút", "Cái", 35000, 52000, 40),
            new("BL003", "Túi hồ sơ có khóa kéo A4", "Balo & Túi bút", "Cái", 15000, 24000, 80),

            new("MI001", "Ruột bút gel 0.5mm (hộp 10)", "Mực & Ruột bút", "Hộp", 25000, 36000, 50),
            new("MI002", "Mực bút máy tím 30ml", "Mực & Ruột bút", "Lọ", 12000, 18000, 65),
            new("MI003", "Ruột chì kim 0.5mm 2B", "Mực & Ruột bút", "Tuýp", 6000, 10000, 120),
            new("MI004", "Mực dấu đỏ 30ml", "Mực & Ruột bút", "Lọ", 15000, 22000, 3),

            new("QT001", "Thiệp chúc mừng 20/11", "Quà tặng & Thiệp", "Cái", 6000, 12000, 150),
            new("QT002", "Giấy gói quà khổ lớn", "Quà tặng & Thiệp", "Tờ", 4000, 8000, 200),
            new("QT003", "Hộp quà kraft size M", "Quà tặng & Thiệp", "Cái", 12000, 20000, 60),
            new("QT004", "Sổ tay quà tặng kèm bút", "Quà tặng & Thiệp", "Bộ", 55000, 79000, 25),
        };

        // Mã vạch EAN-13 giả lập: 893 (mã quốc gia Việt Nam) + 8686 (mã doanh nghiệp mẫu) + 5 số thứ tự + 1 số kiểm tra
        private static string Ean13(int sequence)
        {
            var digits = $"8938686{sequence:D5}";
            var sum = digits.Select((ch, i) => (ch - '0') * (i % 2 == 0 ? 1 : 3)).Sum();
            return digits + (10 - sum % 10) % 10;
        }

        // Mức độ bán chạy (mặc định 2): hàng rẻ, dùng nhiều bán chạy hơn hàng đắt
        private static readonly Dictionary<string, int> Popularity = new()
        {
            ["BUT001"] = 12, ["BUT002"] = 6, ["BUT003"] = 7, ["BUT004"] = 8, ["BUT007"] = 4, ["BUT009"] = 4,
            ["VO001"] = 9, ["VO002"] = 5, ["VO004"] = 7, ["VO006"] = 3,
            ["GIAY001"] = 5, ["GIAY002"] = 3, ["GIAY003"] = 5, ["GIAY005"] = 3,
            ["HT001"] = 5, ["HT002"] = 6, ["HT005"] = 3, ["VP003"] = 4, ["VP004"] = 5, ["VP002"] = 3,
            ["KD001"] = 4, ["KD003"] = 5, ["KD004"] = 5, ["LT002"] = 3, ["MI003"] = 3, ["QT001"] = 3,
            ["MTC001"] = 1, ["MTC002"] = 1, ["BL001"] = 1, ["GIAY004"] = 1, ["GIAY007"] = 1, ["LT005"] = 1,
        };

        private static void SeedCatalog(AppDbContext db)
        {
            var categories = db.Categories.ToDictionary(c => c.CategoryName);
            foreach (var (name, description) in Categories.Where(c => !categories.ContainsKey(c.Name)))
            {
                var c = new Category { CategoryName = name, Description = description };
                db.Categories.Add(c);
                categories[name] = c;
            }

            var existing = db.Products.ToDictionary(p => p.ProductCode);
            var usedBarcodes = existing.Values.Where(p => p.Barcode != null).Select(p => p.Barcode!).ToHashSet();
            for (var i = 0; i < Products.Length; i++)
            {
                var p = Products[i];
                if (existing.TryGetValue(p.Code, out var old))
                {
                    // CSDL tạo trước khi có cột Barcode -> bổ sung mã vạch (nếu mã đó chưa bị dùng)
                    if (old.Barcode == null && usedBarcodes.Add(Ean13(i + 1)))
                        old.Barcode = Ean13(i + 1);
                    continue;
                }
                db.Products.Add(new Product
                {
                    ProductCode = p.Code,
                    Barcode = Ean13(i + 1),
                    ProductName = p.Name,
                    Category = categories[p.Category],
                    Unit = p.Unit,
                    CostPrice = p.Cost,
                    SalePrice = p.Price,
                    StockQuantity = p.Stock
                });
            }
            db.SaveChanges();
        }

        // ===== Khách hàng thân thiết (DaysAgo = đăng ký cách hôm nay bao nhiêu ngày) =====
        private record CustomerSeed(string Name, string Phone, string? Email, string Address, int DaysAgo, int Points = 0);

        private static readonly CustomerSeed[] Customers =
        {
            new("Lê Minh Anh", "0901234567", "minhanh@example.com", "Q.1, TP.HCM", 170, 120),
            new("Phạm Thu Hà", "0912345678", null, "Q.3, TP.HCM", 150, 60),
            new("Công ty TNHH ABC", "02838123456", "vanphong@abc.example", "Q.7, TP.HCM", 140),
            new("Nguyễn Hoàng Nam", "0938111222", "hoangnam@example.com", "Q.Bình Thạnh, TP.HCM", 132),
            new("Trần Ngọc Bích", "0977333444", null, "Q.10, TP.HCM", 125),
            new("Võ Thanh Tùng", "0909555666", "tungvt@example.com", "TP.Thủ Đức, TP.HCM", 118),
            new("Đặng Mỹ Linh", "0866777888", "mylinh.dang@example.com", "Q.5, TP.HCM", 104),
            new("Trường THCS Lê Lợi", "02839998877", "vanthu.leloi@example.edu.vn", "Q.3, TP.HCM", 98),
            new("Bùi Quốc Huy", "0918222333", null, "Q.Gò Vấp, TP.HCM", 91),
            new("Lý Thị Kim Ngân", "0932444555", "kimngan@example.com", "Q.Tân Bình, TP.HCM", 84),
            new("Huỳnh Văn Phúc", "0988666777", null, "Q.8, TP.HCM", 77),
            new("Ngô Bảo Châu Anh", "0703888999", "chauanh.ngo@example.com", "Q.Phú Nhuận, TP.HCM", 70),
            new("Văn phòng Luật Minh Tâm", "02862223344", "lienhe@minhtam.example", "Q.1, TP.HCM", 63),
            new("Phan Gia Hân", "0356111000", null, "Q.Tân Phú, TP.HCM", 56),
            new("Mai Anh Khoa", "0945222111", "khoa.mai@example.com", "Q.11, TP.HCM", 47),
            new("Tô Thị Hồng Nhung", "0397333222", null, "Q.Bình Tân, TP.HCM", 38),
            new("Dương Minh Trí", "0812444333", "tri.duong@example.com", "Q.4, TP.HCM", 26),
            new("Châu Ngọc Yến", "0779555444", null, "Q.6, TP.HCM", 12),
            new("Lâm Quang Vinh", "0868666555", "vinh.lam@example.com", "Q.12, TP.HCM", 6),
            new("Kiều Thanh Thảo", "0326777666", null, "H.Nhà Bè, TP.HCM", 2),
        };

        private static void SeedCustomers(AppDbContext db)
        {
            var existingPhones = db.Customers.Select(c => c.Phone).ToHashSet();
            foreach (var c in Customers.Where(c => !existingPhones.Contains(c.Phone)))
            {
                db.Customers.Add(new Customer
                {
                    FullName = c.Name,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address,
                    Points = c.Points,
                    CreatedAt = DateTime.Today.AddDays(-c.DaysAgo).AddHours(9 + c.DaysAgo % 9)
                });
            }
            db.SaveChanges();
        }

        // ===== Lịch sử nhập hàng + bán hàng ~75 ngày gần nhất (cho biểu đồ, top bán chạy, báo cáo ca) =====
        private const int HistoryDays = 75;

        private static readonly (string Name, string[] Categories)[] Suppliers =
        {
            ("Công ty TNHH Phân phối VPP Phương Nam", new[] { "Bút viết", "Mực & Ruột bút", "Dụng cụ học tập" }),
            ("Công ty CP Giấy An Bình", new[] { "Giấy in & Giấy note", "Vở & Sổ" }),
            ("Công ty TNHH Thiết bị Văn phòng Đông Dương", new[] { "Dụng cụ văn phòng", "Bìa & Lưu trữ hồ sơ", "Băng keo & Keo dán" }),
            ("Đại lý Mỹ thuật Sắc Màu", new[] { "Mỹ thuật", "Quà tặng & Thiệp" }),
            ("Nhà phân phối Thiết bị học đường Tân Tiến", new[] { "Máy tính cầm tay", "Balo & Túi bút", "Dụng cụ học tập" }),
        };

        private static void SeedHistory(AppDbContext db)
        {
            // Đơn mẫu có mã dạng HDyyMMdd-NNN, phiếu nhập mẫu PNyyMMdd-NN -> đã có thì không tạo lại
            if (db.Orders.Any(o => o.OrderCode.Contains("-")) || db.ImportReceipts.Any(r => r.ReceiptCode.Contains("-"))) return;

            var rnd = new Random(2026);   // cố định seed -> lần nào tạo cũng ra cùng một bộ dữ liệu
            var today = DateTime.Today;
            var now = DateTime.Now;
            var start = today.AddDays(-HistoryDays);

            var users = db.Users.ToDictionary(u => u.Username);
            var admin = users["admin"];
            var cashiers = new[] { (users["nhanvien"], 4), (users["tuanpm"], 3), (users["landt"], 3), (admin, 1) };

            var products = db.Products.Include(p => p.Category).Where(p => p.IsActive).ToList();
            var weighted = products.Select(p => (p, Popularity.GetValueOrDefault(p.ProductCode, 2))).ToArray();
            var customers = db.Customers.ToList();
            // Khách mẫu tạo từ phiên bản seed cũ có ngày đăng ký = ngày chạy lần đầu -> lùi về đúng mốc để có lịch sử mua hàng
            foreach (var c in customers)
            {
                var seed = Customers.FirstOrDefault(s => s.Phone == c.Phone);
                var registeredAt = today.AddDays(-(seed?.DaysAgo ?? 0)).AddHours(9);
                if (seed != null && c.CreatedAt > registeredAt) c.CreatedAt = registeredAt;
            }
            var points = customers.ToDictionary(c => c.CustomerId, c => c.Points);

            // --- Phiếu nhập: khoảng 3-4 ngày một phiếu ---
            var receiptsPerDay = new Dictionary<DateTime, int>();
            for (var day = start.AddDays(1); day <= today; day = day.AddDays(rnd.Next(3, 5)))
            {
                var (supplier, cats) = Suppliers[rnd.Next(Suppliers.Length)];
                var candidates = products.Where(p => cats.Contains(p.Category!.CategoryName)).OrderBy(_ => rnd.Next()).Take(rnd.Next(3, 7)).ToList();
                var createdAt = day.AddHours(rnd.Next(7, 10)).AddMinutes(rnd.Next(60));
                if (createdAt > now || candidates.Count == 0) continue;

                var seq = receiptsPerDay[day] = receiptsPerDay.GetValueOrDefault(day) + 1;
                var receipt = new ImportReceipt
                {
                    ReceiptCode = $"PN{day:yyMMdd}-{seq:D2}",
                    CreatedAt = createdAt,
                    UserId = admin.UserId,
                    SupplierName = supplier,
                    Note = rnd.Next(4) == 0 ? "Nhập bổ sung hàng sắp hết" : null
                };
                foreach (var p in candidates)
                {
                    var qty = p.CostPrice >= 100_000 ? rnd.Next(1, 3) * 5 : p.CostPrice >= 30_000 ? rnd.Next(2, 6) * 10 : rnd.Next(5, 21) * 10;
                    receipt.Details.Add(new ImportReceiptDetail { ProductId = p.ProductId, Quantity = qty, UnitCost = p.CostPrice, LineTotal = qty * p.CostPrice });
                }
                receipt.TotalAmount = receipt.Details.Sum(d => d.LineTotal);
                db.ImportReceipts.Add(receipt);
            }

            // --- Hóa đơn bán hàng ---
            int[] hourWeights = { 3, 6, 7, 5, 4, 3, 4, 6, 8, 8, 7, 5, 3, 2 };   // 7h..20h: cao điểm sáng & chiều tan học
            for (var day = start; day <= today; day = day.AddDays(1))
            {
                var dayIndex = (day - start).Days;
                double expected = 4 + dayIndex * 0.04;                                         // tăng trưởng nhẹ theo thời gian
                if (day.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) expected *= 1.5;
                if ((day.Month == 8 && day.Day >= 20) || (day.Month == 9 && day.Day <= 8)) expected *= 2.2;   // mùa tựu trường
                if (day.Month == 11 && day.Day >= 10 && day.Day <= 19) expected *= 1.6;      // mùa 20/11
                var count = Math.Max(1, (int)Math.Round(expected * (0.75 + rnd.NextDouble() * 0.5)));

                var times = Enumerable.Range(0, count)
                    .Select(_ => day.AddHours(7 + Pick(rnd, hourWeights.Select((w, i) => (i, w)).ToArray())).AddMinutes(rnd.Next(60)).AddSeconds(rnd.Next(60)))
                    .Where(t => t < now)
                    .OrderBy(t => t).ToList();

                for (var i = 0; i < times.Count; i++)
                {
                    var createdAt = times[i];
                    var order = new Order
                    {
                        OrderCode = $"HD{day:yyMMdd}-{i + 1:D3}",
                        CreatedAt = createdAt,
                        UserId = Pick(rnd, cashiers).UserId,
                        Status = rnd.Next(100) < 3 ? OrderStatus.Cancelled : OrderStatus.Completed
                    };

                    // Khoảng 45% hóa đơn là khách thân thiết (đã đăng ký trước thời điểm mua)
                    Customer? customer = null;
                    var registered = customers.Where(c => c.CreatedAt < createdAt).ToList();
                    if (registered.Count > 0 && rnd.Next(100) < 45) customer = registered[rnd.Next(registered.Count)];
                    var isOrganization = customer != null && customer.Phone.StartsWith("028");
                    order.CustomerId = customer?.CustomerId;

                    // Chi tiết: 1-5 mặt hàng, tổ chức (công ty, trường học) mua số lượng lớn
                    var lineCount = isOrganization ? rnd.Next(3, 7) : Pick(rnd, new[] { (1, 4), (2, 4), (3, 3), (4, 2), (5, 1) });
                    foreach (var p in Enumerable.Range(0, lineCount).Select(_ => Pick(rnd, weighted)).DistinctBy(p => p.ProductId))
                    {
                        var qty = p.SalePrice >= 100_000 ? 1 : p.SalePrice >= 30_000 ? rnd.Next(1, 3) : rnd.Next(1, 5);
                        if (isOrganization) qty *= rnd.Next(5, 11);
                        order.Details.Add(new OrderDetail
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            UnitPrice = p.SalePrice,
                            UnitCost = p.CostPrice,
                            Quantity = qty,
                            LineTotal = p.SalePrice * qty
                        });
                    }
                    order.SubTotal = order.Details.Sum(d => d.LineTotal);

                    // Giảm giá tay cho hóa đơn lớn
                    if (order.SubTotal >= 150_000 && rnd.Next(100) < 15)
                        order.Discount = Math.Round(order.SubTotal * rnd.Next(3, 8) / 100 / 1000) * 1000;

                    // Đổi điểm: khách có >= 30 điểm, thỉnh thoảng dùng 20-60 điểm (1 điểm = 1.000đ)
                    if (customer != null && order.Status == OrderStatus.Completed && points[customer.CustomerId] >= 30 && rnd.Next(100) < 25)
                    {
                        var maxByMoney = (int)((order.SubTotal - order.Discount - 1000) / AdoNetRepository.PointValue);
                        order.PointsUsed = Math.Max(0, Math.Min(Math.Min(points[customer.CustomerId], rnd.Next(2, 7) * 10), maxByMoney));
                        order.PointsDiscount = order.PointsUsed * AdoNetRepository.PointValue;
                    }

                    order.TotalAmount = order.SubTotal - order.Discount - order.PointsDiscount;
                    var unit = Pick(rnd, new[] { (1_000m, 3), (10_000m, 3), (50_000m, 3), (100_000m, 2), (500_000m, 1) });
                    order.CustomerPaid = Math.Ceiling(order.TotalAmount / unit) * unit;
                    if (rnd.Next(100) < 5) order.Note = isOrganization ? "Xuất hóa đơn VAT" : "Khách đặt trước qua điện thoại";

                    if (customer != null && order.Status == OrderStatus.Completed)
                        points[customer.CustomerId] += (int)(order.TotalAmount / AdoNetRepository.MoneyPerPoint) - order.PointsUsed;

                    db.Orders.Add(order);
                }
            }

            foreach (var c in customers) c.Points = points[c.CustomerId];
            db.SaveChanges();
        }

        // Chọn ngẫu nhiên có trọng số
        private static T Pick<T>(Random rnd, (T Item, int Weight)[] items)
        {
            var roll = rnd.Next(items.Sum(x => x.Weight));
            foreach (var (item, weight) in items)
            {
                if (roll < weight) return item;
                roll -= weight;
            }
            return items[^1].Item;
        }
    }
}
