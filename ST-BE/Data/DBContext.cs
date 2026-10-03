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
            public DbSet<Customers> Customers { get; set; }

        // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nạp sẵn 5 danh mục ban đầu vào SQL Server ngay khi tạo bảng
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bút & Dụng cụ viết", Description = "Bút bi, bút chì, bút gel, bút dạ" },
                new Category { CategoryId = 2, CategoryName = "Giấy & Sổ", Description = "Giấy in, giấy note, sổ tay, tập vở" },
                new Category { CategoryId = 3, CategoryName = "Dụng cụ học tập", Description = "Thước, compa, tẩy, gọt bút chì" },
                new Category { CategoryId = 4, CategoryName = "Dụng cụ văn phòng", Description = "Kéo, bấm kim, kẹp giấy, dao rọc giấy" },
                new Category { CategoryId = 5, CategoryName = "Lưu trữ & Hồ sơ", Description = "Bìa hồ sơ, file tài liệu, hộp đựng hồ sơ" }
            );
            modelBuilder.Entity<Customers>().HasData(
                new Customers
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    PhoneNumber = "0901122334",
                    Address = "123 Nguyễn Trãi, Quận 1, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 150
                },
                new Customers
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị B",
                    PhoneNumber = "0918877665",
                    Address = "45 Lê Lợi, Quận 1, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 50
                },
                new Customers
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn C",
                    PhoneNumber = "0983344556",
                    Address = "78 Điện Biên Phủ, Bình Thạnh, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 10
                },
                new Customers
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị D",
                    PhoneNumber = "0905234167",
                    Address = "56 Nguyễn Đình Chiểu, Quận 3, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 200
                },
                new Customers
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Văn E",
                    PhoneNumber = "0916342789",
                    Address = "89 Cách Mạng Tháng Tám, Quận 10, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 80
                },
                new Customers
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị F",
                    PhoneNumber = "0987456123",
                    Address = "12 Phan Văn Trị, Gò Vấp, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 25
                },
                new Customers
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Văn G",
                    PhoneNumber = "0908765432",
                    Address = "234 Phạm Văn Đồng, Thủ Đức, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 175
                },
                new Customers
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Thị H",
                    PhoneNumber = "0912345678",
                    Address = "67 Hoàng Văn Thụ, Tân Bình, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 65
                },
                new Customers
                {
                    CustomerId = 9,
                    CustomerName = "Đỗ Văn I",
                    PhoneNumber = "0987654321",
                    Address = "145 Nguyễn Văn Cừ, Quận 5, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 15
                },
                new Customers
                {
                    CustomerId = 10,
                    CustomerName = "Ngô Thị K",
                    PhoneNumber = "0903456789",
                    Address = "90 Võ Văn Tần, Quận 3, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 250
                },
                new Customers
                {
                    CustomerId = 11,
                    CustomerName = "Phan Văn L",
                    PhoneNumber = "0914567890",
                    Address = "34 Lũy Bán Bích, Tân Phú, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 95
                },
                new Customers
                {
                    CustomerId = 12,
                    CustomerName = "Huỳnh Thị M",
                    PhoneNumber = "0981234567",
                    Address = "76 Nguyễn Oanh, Gò Vấp, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 30
                },
                new Customers
                {
                    CustomerId = 13,
                    CustomerName = "Trương Văn N",
                    PhoneNumber = "0909876543",
                    Address = "156 Nguyễn Hữu Thọ, Quận 7, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 320
                },
                new Customers
                {
                    CustomerId = 14,
                    CustomerName = "Lý Thị O",
                    PhoneNumber = "0917654321",
                    Address = "48 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 70
                },
                new Customers
                {
                    CustomerId = 15,
                    CustomerName = "Mai Văn P",
                    PhoneNumber = "0984561230",
                    Address = "201 Trường Chinh, Tân Bình, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 20
                },

                new Customers
                {
                    CustomerId = 16,
                    CustomerName = "Nguyễn Thị Q",
                    PhoneNumber = "0901234567",
                    Address = "25 Nguyễn Thị Minh Khai, Quận 1, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 180
                },
                new Customers
                {
                    CustomerId = 17,
                    CustomerName = "Trần Văn R",
                    PhoneNumber = "0912348901",
                    Address = "63 Âu Cơ, Tân Phú, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 85
                },
                new Customers
                {
                    CustomerId = 18,
                    CustomerName = "Lê Thị S",
                    PhoneNumber = "0983456781",
                    Address = "112 Nguyễn Kiệm, Phú Nhuận, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 35
                },
                new Customers
                {
                    CustomerId = 19,
                    CustomerName = "Phạm Văn T",
                    PhoneNumber = "0904567891",
                    Address = "87 Nguyễn Văn Linh, Quận 7, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 220
                },
                new Customers
                {
                    CustomerId = 20,
                    CustomerName = "Hoàng Thị U",
                    PhoneNumber = "0915678902",
                    Address = "42 Lê Văn Sỹ, Quận 3, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 60
                },
                new Customers
                {
                    CustomerId = 21,
                    CustomerName = "Võ Văn V",
                    PhoneNumber = "0986789012",
                    Address = "19 Tô Ngọc Vân, Thủ Đức, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 18
                },
                new Customers
                {
                    CustomerId = 22,
                    CustomerName = "Đặng Thị X",
                    PhoneNumber = "0907890123",
                    Address = "88 Nguyễn Thái Học, Quận 1, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 275
                },
                new Customers
                {
                    CustomerId = 23,
                    CustomerName = "Bùi Văn Y",
                    PhoneNumber = "0918901234",
                    Address = "135 Quang Trung, Gò Vấp, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 110
                },
                new Customers
                {
                    CustomerId = 24,
                    CustomerName = "Đỗ Thị Z",
                    PhoneNumber = "0989012345",
                    Address = "29 Hồng Bàng, Quận 5, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 40
                },
                new Customers
                {
                    CustomerId = 25,
                    CustomerName = "Nguyễn Văn Hùng",
                    PhoneNumber = "0902345678",
                    Address = "74 Pasteur, Quận 1, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 350
                },
                new Customers
                {
                    CustomerId = 26,
                    CustomerName = "Trần Thị Lan",
                    PhoneNumber = "0913456789",
                    Address = "158 Lê Văn Việt, Thủ Đức, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 75
                },
                new Customers
                {
                    CustomerId = 27,
                    CustomerName = "Lê Văn Minh",
                    PhoneNumber = "0984567891",
                    Address = "91 Nguyễn Trãi, Quận 5, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 22
                },
                new Customers
                {
                    CustomerId = 28,
                    CustomerName = "Phạm Thị Hoa",
                    PhoneNumber = "0905678901",
                    Address = "36 Phan Đình Phùng, Phú Nhuận, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 190
                },
                new Customers
                {
                    CustomerId = 29,
                    CustomerName = "Hoàng Văn Nam",
                    PhoneNumber = "0916789012",
                    Address = "205 Kinh Dương Vương, Bình Tân, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 90
                },
                new Customers
                {
                    CustomerId = 30,
                    CustomerName = "Võ Thị Mai",
                    PhoneNumber = "0987890123",
                    Address = "64 Nguyễn Sơn, Tân Phú, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 28
                },

                new Customers
                {
                    CustomerId = 31,
                    CustomerName = "Đặng Văn Sơn",
                    PhoneNumber = "0908901234",
                    Address = "105 Hai Bà Trưng, Quận 1, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 240
                },
                new Customers
                {
                    CustomerId = 32,
                    CustomerName = "Bùi Thị Ngọc",
                    PhoneNumber = "0919012345",
                    Address = "73 Nguyễn Văn Đậu, Bình Thạnh, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 100
                },
                new Customers
                {
                    CustomerId = 33,
                    CustomerName = "Đỗ Văn Thành",
                    PhoneNumber = "0980123456",
                    Address = "48 Lạc Long Quân, Tân Bình, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 12
                },
                new Customers
                {
                    CustomerId = 34,
                    CustomerName = "Nguyễn Thị Hương",
                    PhoneNumber = "0901237890",
                    Address = "120 Nguyễn Đình Chiểu, Quận 3, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 310
                },
                new Customers
                {
                    CustomerId = 35,
                    CustomerName = "Trần Văn Bình",
                    PhoneNumber = "0912348902",
                    Address = "58 Lê Đức Thọ, Gò Vấp, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 120
                },
                new Customers
                {
                    CustomerId = 36,
                    CustomerName = "Lê Thị Thảo",
                    PhoneNumber = "0983459012",
                    Address = "93 Nguyễn Hữu Cảnh, Bình Thạnh, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 45
                },
                new Customers
                {
                    CustomerId = 37,
                    CustomerName = "Phạm Văn Long",
                    PhoneNumber = "0904560123",
                    Address = "167 Quốc lộ 13, Thủ Đức, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 280
                },
                new Customers
                {
                    CustomerId = 38,
                    CustomerName = "Hoàng Thị Vân",
                    PhoneNumber = "0915671234",
                    Address = "25 Trần Huy Liệu, Phú Nhuận, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 55
                },
                new Customers
                {
                    CustomerId = 39,
                    CustomerName = "Võ Văn Đức",
                    PhoneNumber = "0986782345",
                    Address = "82 Nguyễn Văn Quá, Quận 12, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 32
                },
                new Customers
                {
                    CustomerId = 40,
                    CustomerName = "Đặng Thị Nga",
                    PhoneNumber = "0907893456",
                    Address = "45 Nam Kỳ Khởi Nghĩa, Quận 3, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 365
                },
                new Customers
                {
                    CustomerId = 41,
                    CustomerName = "Bùi Văn Phúc",
                    PhoneNumber = "0918904567",
                    Address = "78 Nguyễn Thị Thập, Quận 7, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 130
                },
                new Customers
                {
                    CustomerId = 42,
                    CustomerName = "Đỗ Thị Yến",
                    PhoneNumber = "0989015678",
                    Address = "16 Bình Long, Tân Phú, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 16
                },
                new Customers
                {
                    CustomerId = 43,
                    CustomerName = "Nguyễn Văn Khoa",
                    PhoneNumber = "0902346789",
                    Address = "99 Võ Văn Ngân, Thủ Đức, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 210
                },
                new Customers
                {
                    CustomerId = 44,
                    CustomerName = "Trần Thị Nhung",
                    PhoneNumber = "0913457890",
                    Address = "52 Nguyễn Văn Trỗi, Phú Nhuận, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 105
                },
                new Customers
                {
                    CustomerId = 45,
                    CustomerName = "Lê Văn Dũng",
                    PhoneNumber = "0984568901",
                    Address = "134 Lê Văn Quới, Bình Tân, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 38
                }
            );


            modelBuilder.Entity<Product>().HasData(
               // Category 1 - Bút & Dụng cụ viết
               new Product { ProductId = 1, Barcode = "8938501234001", ProductName = "Bút bi Thiên Long TL-027", Price = 5000, StockQuantity = 100, CategoryId = 1 },
               new Product { ProductId = 2, Barcode = "8938501234002", ProductName = "Bút gel Thiên Long GEL-08", Price = 12000, StockQuantity = 80, CategoryId = 1 },
               new Product { ProductId = 3, Barcode = "8938501234003", ProductName = "Bút chì gỗ 2B", Price = 4000, StockQuantity = 120, CategoryId = 1 },
               new Product { ProductId = 4, Barcode = "8938501234004", ProductName = "Bút dạ quang vàng", Price = 10000, StockQuantity = 70, CategoryId = 1 },
               new Product { ProductId = 5, Barcode = "8938501234005", ProductName = "Bút lông dầu màu đen", Price = 15000, StockQuantity = 50, CategoryId = 1 },
               new Product { ProductId = 6, Barcode = "8938501234006", ProductName = "Bút máy học sinh", Price = 25000, StockQuantity = 45, CategoryId = 1 },
               new Product { ProductId = 7, Barcode = "8938501234007", ProductName = "Ruột bút bi xanh", Price = 3000, StockQuantity = 150, CategoryId = 1 },
               new Product { ProductId = 8, Barcode = "8938501234008", ProductName = "Bút marker xanh", Price = 12000, StockQuantity = 65, CategoryId = 1 },
               new Product { ProductId = 9, Barcode = "8938501234009", ProductName = "Bút chì kim 0.5mm", Price = 18000, StockQuantity = 55, CategoryId = 1 },

               // Category 2 - Giấy & Sổ
               new Product { ProductId = 10, Barcode = "8938501234010", ProductName = "Giấy A4 Double A 70gsm", Price = 75000, StockQuantity = 50, CategoryId = 2 },
               new Product { ProductId = 11, Barcode = "8938501234011", ProductName = "Giấy note 3x3 màu vàng", Price = 15000, StockQuantity = 70, CategoryId = 2 },
               new Product { ProductId = 12, Barcode = "8938501234012", ProductName = "Sổ tay lò xo A5", Price = 35000, StockQuantity = 45, CategoryId = 2 },
               new Product { ProductId = 13, Barcode = "8938501234013", ProductName = "Giấy A5 trắng 70gsm", Price = 45000, StockQuantity = 60, CategoryId = 2 },
               new Product { ProductId = 14, Barcode = "8938501234014", ProductName = "Sổ tay bìa cứng A5", Price = 55000, StockQuantity = 35, CategoryId = 2 },
               new Product { ProductId = 15, Barcode = "8938501234015", ProductName = "Tập vở 200 trang", Price = 22000, StockQuantity = 90, CategoryId = 2 },
               new Product { ProductId = 16, Barcode = "8938501234016", ProductName = "Giấy in màu A4", Price = 65000, StockQuantity = 40, CategoryId = 2 },
               new Product { ProductId = 17, Barcode = "8938501234017", ProductName = "Sổ lò xo A4", Price = 48000, StockQuantity = 30, CategoryId = 2 },
               new Product { ProductId = 18, Barcode = "8938501234018", ProductName = "Giấy note nhiều màu", Price = 20000, StockQuantity = 75, CategoryId = 2 },

               // Category 3 - Dụng cụ học tập
               new Product { ProductId = 19, Barcode = "8938501234019", ProductName = "Thước kẻ 20cm", Price = 7000, StockQuantity = 90, CategoryId = 3 },
               new Product { ProductId = 20, Barcode = "8938501234020", ProductName = "Compa học sinh", Price = 25000, StockQuantity = 40, CategoryId = 3 },
               new Product { ProductId = 21, Barcode = "8938501234021", ProductName = "Tẩy chì trắng Thiên Long", Price = 5000, StockQuantity = 100, CategoryId = 3 },
               new Product { ProductId = 22, Barcode = "8938501234022", ProductName = "Gọt bút chì 2 lỗ", Price = 8000, StockQuantity = 80, CategoryId = 3 },
               new Product { ProductId = 23, Barcode = "8938501234023", ProductName = "Thước tam giác 20cm", Price = 12000, StockQuantity = 50, CategoryId = 3 },
               new Product { ProductId = 24, Barcode = "8938501234024", ProductName = "Bộ thước học sinh", Price = 30000, StockQuantity = 45, CategoryId = 3 },
               new Product { ProductId = 25, Barcode = "8938501234025", ProductName = "Hộp bút học sinh", Price = 35000, StockQuantity = 60, CategoryId = 3 },
               new Product { ProductId = 26, Barcode = "8938501234026", ProductName = "Màu sáp 12 màu", Price = 28000, StockQuantity = 55, CategoryId = 3 },
               new Product { ProductId = 27, Barcode = "8938501234027", ProductName = "Bộ bút màu 12 màu", Price = 40000, StockQuantity = 35, CategoryId = 3 },

               // Category 4 - Dụng cụ văn phòng
               new Product { ProductId = 28, Barcode = "8938501234028", ProductName = "Kéo văn phòng 18cm", Price = 20000, StockQuantity = 60, CategoryId = 4 },
               new Product { ProductId = 29, Barcode = "8938501234029", ProductName = "Bấm kim số 10", Price = 30000, StockQuantity = 35, CategoryId = 4 },
               new Product { ProductId = 30, Barcode = "8938501234030", ProductName = "Hộp kẹp giấy 100 cái", Price = 18000, StockQuantity = 55, CategoryId = 4 },
               new Product { ProductId = 31, Barcode = "8938501234031", ProductName = "Dao rọc giấy nhỏ", Price = 15000, StockQuantity = 45, CategoryId = 4 },
               new Product { ProductId = 32, Barcode = "8938501234032", ProductName = "Băng keo trong 2cm", Price = 10000, StockQuantity = 70, CategoryId = 4 },
               new Product { ProductId = 33, Barcode = "8938501234033", ProductName = "Keo dán giấy", Price = 12000, StockQuantity = 80, CategoryId = 4 },
               new Product { ProductId = 34, Barcode = "8938501234034", ProductName = "Ghim bấm số 10", Price = 8000, StockQuantity = 100, CategoryId = 4 },
               new Product { ProductId = 35, Barcode = "8938501234035", ProductName = "Kẹp bướm 32mm", Price = 15000, StockQuantity = 65, CategoryId = 4 },
               new Product { ProductId = 36, Barcode = "8938501234036", ProductName = "Bộ ghim giấy văn phòng", Price = 22000, StockQuantity = 50, CategoryId = 4 },

               // Category 5 - Lưu trữ & Hồ sơ
               new Product { ProductId = 37, Barcode = "8938501234037", ProductName = "Bìa hồ sơ A4 nút bấm", Price = 8000, StockQuantity = 100, CategoryId = 5 },
               new Product { ProductId = 38, Barcode = "8938501234038", ProductName = "File tài liệu A4 20 lá", Price = 25000, StockQuantity = 50, CategoryId = 5 },
               new Product { ProductId = 39, Barcode = "8938501234039", ProductName = "Hộp đựng hồ sơ A4", Price = 45000, StockQuantity = 30, CategoryId = 5 },
               new Product { ProductId = 40, Barcode = "8938501234040", ProductName = "Bìa còng A4", Price = 35000, StockQuantity = 45, CategoryId = 5 },
               new Product { ProductId = 41, Barcode = "8938501234041", ProductName = "File lá A4 40 lá", Price = 40000, StockQuantity = 55, CategoryId = 5 },
               new Product { ProductId = 42, Barcode = "8938501234042", ProductName = "Bìa trình ký A4", Price = 30000, StockQuantity = 40, CategoryId = 5 },
               new Product { ProductId = 43, Barcode = "8938501234043", ProductName = "Túi hồ sơ A4", Price = 7000, StockQuantity = 90, CategoryId = 5 },
               new Product { ProductId = 44, Barcode = "8938501234044", ProductName = "Hộp lưu trữ tài liệu", Price = 55000, StockQuantity = 25, CategoryId = 5 },
               new Product { ProductId = 45, Barcode = "8938501234045", ProductName = "Bìa hồ sơ có dây", Price = 10000, StockQuantity = 75, CategoryId = 5 }
           );

        }
    }
    }
