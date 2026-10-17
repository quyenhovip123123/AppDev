using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ST_BE.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bút & Dụng cụ viết", "Bút bi, bút chì, bút gel, bút dạ" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Giấy & Sổ", "Giấy in, giấy note, sổ tay, tập vở" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Dụng cụ học tập", "Thước, compa, tẩy, gọt bút chì" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Dụng cụ văn phòng", "Kéo, bấm kim, kẹp giấy, dao rọc giấy" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Lưu trữ & Hồ sơ", "Bìa hồ sơ, file tài liệu, hộp đựng hồ sơ" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 4, null, "Phạm Thị D", "Vàng", "0905234167", 200 },
                    { 5, null, "Hoàng Văn E", "Bạc", "0916342789", 80 },
                    { 6, null, "Võ Thị F", "Chuẩn", "0987456123", 25 },
                    { 7, null, "Đặng Văn G", "Vàng", "0908765432", 175 },
                    { 8, null, "Bùi Thị H", "Bạc", "0912345678", 65 },
                    { 9, null, "Đỗ Văn I", "Chuẩn", "0987654321", 15 },
                    { 10, null, "Ngô Thị K", "Vàng", "0903456789", 250 },
                    { 11, null, "Phan Văn L", "Bạc", "0914567890", 95 },
                    { 12, null, "Huỳnh Thị M", "Chuẩn", "0981234567", 30 },
                    { 13, null, "Trương Văn N", "Vàng", "0909876543", 320 },
                    { 14, null, "Lý Thị O", "Bạc", "0917654321", 70 },
                    { 15, null, "Mai Văn P", "Chuẩn", "0984561230", 20 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "8938501234001", 1, 5000m, "Bút bi Thiên Long TL-027", 100 },
                    { 2, "8938501234002", 1, 12000m, "Bút gel Thiên Long GEL-08", 80 },
                    { 3, "8938501234003", 1, 4000m, "Bút chì gỗ 2B", 120 },
                    { 4, "8938501234004", 2, 75000m, "Giấy A4 Double A 70gsm", 50 },
                    { 5, "8938501234005", 2, 15000m, "Giấy note 3x3 màu vàng", 70 },
                    { 6, "8938501234006", 2, 35000m, "Sổ tay lò xo A5", 45 },
                    { 7, "8938501234007", 3, 7000m, "Thước kẻ 20cm", 90 },
                    { 8, "8938501234008", 3, 25000m, "Compa học sinh", 40 },
                    { 9, "8938501234009", 3, 5000m, "Tẩy chì trắng Thiên Long", 100 },
                    { 10, "8938501234010", 4, 20000m, "Kéo văn phòng 18cm", 60 },
                    { 11, "8938501234011", 4, 30000m, "Bấm kim số 10", 35 },
                    { 12, "8938501234012", 4, 18000m, "Hộp kẹp giấy 100 cái", 55 },
                    { 13, "8938501234013", 5, 8000m, "Bìa hồ sơ A4 nút bấm", 100 },
                    { 14, "8938501234014", 5, 25000m, "File tài liệu A4 20 lá", 50 },
                    { 15, "8938501234015", 5, 45000m, "Hộp đựng hồ sơ A4", 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo dẻo" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị & Dầu ăn", "Nước mắm, hạt nêm, dầu thực vật" });
        }
    }
}
