using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ST_BE.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 16, null, "Nguyễn Thị Q", "Vàng", "0901234567", 180 },
                    { 17, null, "Trần Văn R", "Bạc", "0912348901", 85 },
                    { 18, null, "Lê Thị S", "Chuẩn", "0983456781", 35 },
                    { 19, null, "Phạm Văn T", "Vàng", "0904567891", 220 },
                    { 20, null, "Hoàng Thị U", "Bạc", "0915678902", 60 },
                    { 21, null, "Võ Văn V", "Chuẩn", "0986789012", 18 },
                    { 22, null, "Đặng Thị X", "Vàng", "0907890123", 275 },
                    { 23, null, "Bùi Văn Y", "Bạc", "0918901234", 110 },
                    { 24, null, "Đỗ Thị Z", "Chuẩn", "0989012345", 40 },
                    { 25, null, "Nguyễn Văn Hùng", "Vàng", "0902345678", 350 },
                    { 26, null, "Trần Thị Lan", "Bạc", "0913456789", 75 },
                    { 27, null, "Lê Văn Minh", "Chuẩn", "0984567891", 22 },
                    { 28, null, "Phạm Thị Hoa", "Vàng", "0905678901", 190 },
                    { 29, null, "Hoàng Văn Nam", "Bạc", "0916789012", 90 },
                    { 30, null, "Võ Thị Mai", "Chuẩn", "0987890123", 28 },
                    { 31, null, "Đặng Văn Sơn", "Vàng", "0908901234", 240 },
                    { 32, null, "Bùi Thị Ngọc", "Bạc", "0919012345", 100 },
                    { 33, null, "Đỗ Văn Thành", "Chuẩn", "0980123456", 12 },
                    { 34, null, "Nguyễn Thị Hương", "Vàng", "0901237890", 310 },
                    { 35, null, "Trần Văn Bình", "Bạc", "0912348902", 120 },
                    { 36, null, "Lê Thị Thảo", "Chuẩn", "0983459012", 45 },
                    { 37, null, "Phạm Văn Long", "Vàng", "0904560123", 280 },
                    { 38, null, "Hoàng Thị Vân", "Bạc", "0915671234", 55 },
                    { 39, null, "Võ Văn Đức", "Chuẩn", "0986782345", 32 },
                    { 40, null, "Đặng Thị Nga", "Vàng", "0907893456", 365 },
                    { 41, null, "Bùi Văn Phúc", "Bạc", "0918904567", 130 },
                    { 42, null, "Đỗ Thị Yến", "Chuẩn", "0989015678", 16 },
                    { 43, null, "Nguyễn Văn Khoa", "Vàng", "0902346789", 210 },
                    { 44, null, "Trần Thị Nhung", "Bạc", "0913457890", 105 },
                    { 45, null, "Lê Văn Dũng", "Chuẩn", "0984568901", 38 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 10000m, "Bút dạ quang vàng", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "ProductName", "StockQuantity" },
                values: new object[] { 1, "Bút lông dầu màu đen", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Price", "ProductName" },
                values: new object[] { 1, 25000m, "Bút máy học sinh" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 3000m, "Ruột bút bi xanh", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 12000m, "Bút marker xanh", 65 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 18000m, "Bút chì kim 0.5mm", 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 75000m, "Giấy A4 Double A 70gsm", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 15000m, "Giấy note 3x3 màu vàng", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 35000m, "Sổ tay lò xo A5", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 45000m, "Giấy A5 trắng 70gsm", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 55000m, "Sổ tay bìa cứng A5", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 22000m, "Tập vở 200 trang", 90 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 16, "8938501234016", 2, 65000m, "Giấy in màu A4", 40 },
                    { 17, "8938501234017", 2, 48000m, "Sổ lò xo A4", 30 },
                    { 18, "8938501234018", 2, 20000m, "Giấy note nhiều màu", 75 },
                    { 19, "8938501234019", 3, 7000m, "Thước kẻ 20cm", 90 },
                    { 20, "8938501234020", 3, 25000m, "Compa học sinh", 40 },
                    { 21, "8938501234021", 3, 5000m, "Tẩy chì trắng Thiên Long", 100 },
                    { 22, "8938501234022", 3, 8000m, "Gọt bút chì 2 lỗ", 80 },
                    { 23, "8938501234023", 3, 12000m, "Thước tam giác 20cm", 50 },
                    { 24, "8938501234024", 3, 30000m, "Bộ thước học sinh", 45 },
                    { 25, "8938501234025", 3, 35000m, "Hộp bút học sinh", 60 },
                    { 26, "8938501234026", 3, 28000m, "Màu sáp 12 màu", 55 },
                    { 27, "8938501234027", 3, 40000m, "Bộ bút màu 12 màu", 35 },
                    { 28, "8938501234028", 4, 20000m, "Kéo văn phòng 18cm", 60 },
                    { 29, "8938501234029", 4, 30000m, "Bấm kim số 10", 35 },
                    { 30, "8938501234030", 4, 18000m, "Hộp kẹp giấy 100 cái", 55 },
                    { 31, "8938501234031", 4, 15000m, "Dao rọc giấy nhỏ", 45 },
                    { 32, "8938501234032", 4, 10000m, "Băng keo trong 2cm", 70 },
                    { 33, "8938501234033", 4, 12000m, "Keo dán giấy", 80 },
                    { 34, "8938501234034", 4, 8000m, "Ghim bấm số 10", 100 },
                    { 35, "8938501234035", 4, 15000m, "Kẹp bướm 32mm", 65 },
                    { 36, "8938501234036", 4, 22000m, "Bộ ghim giấy văn phòng", 50 },
                    { 37, "8938501234037", 5, 8000m, "Bìa hồ sơ A4 nút bấm", 100 },
                    { 38, "8938501234038", 5, 25000m, "File tài liệu A4 20 lá", 50 },
                    { 39, "8938501234039", 5, 45000m, "Hộp đựng hồ sơ A4", 30 },
                    { 40, "8938501234040", 5, 35000m, "Bìa còng A4", 45 },
                    { 41, "8938501234041", 5, 40000m, "File lá A4 40 lá", 55 },
                    { 42, "8938501234042", 5, 30000m, "Bìa trình ký A4", 40 },
                    { 43, "8938501234043", 5, 7000m, "Túi hồ sơ A4", 90 },
                    { 44, "8938501234044", 5, 55000m, "Hộp lưu trữ tài liệu", 25 },
                    { 45, "8938501234045", 5, 10000m, "Bìa hồ sơ có dây", 75 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 2, 75000m, "Giấy A4 Double A 70gsm", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "ProductName", "StockQuantity" },
                values: new object[] { 2, "Giấy note 3x3 màu vàng", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Price", "ProductName" },
                values: new object[] { 2, 35000m, "Sổ tay lò xo A5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 3, 7000m, "Thước kẻ 20cm", 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 3, 25000m, "Compa học sinh", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 3, 5000m, "Tẩy chì trắng Thiên Long", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 4, 20000m, "Kéo văn phòng 18cm", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 4, 30000m, "Bấm kim số 10", 35 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 4, 18000m, "Hộp kẹp giấy 100 cái", 55 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 5, 8000m, "Bìa hồ sơ A4 nút bấm", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 5, 25000m, "File tài liệu A4 20 lá", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 5, 45000m, "Hộp đựng hồ sơ A4", 30 });
        }
    }
}
