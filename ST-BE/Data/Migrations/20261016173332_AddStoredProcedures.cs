using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST_BE.Data.Migrations
{
    /// <summary>
    /// Tạo các Stored Procedure báo cáo trên SQL Server (gọi từ C# bằng CommandType.StoredProcedure trong AdoNetRepository)
    /// </summary>
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Doanh thu theo ca: tổng hợp theo nhân viên trong khoảng [@From, @To)
            migrationBuilder.Sql(@"
CREATE OR ALTER PROCEDURE dbo.sp_RevenueByShift
    @From   DATETIME2,
    @To     DATETIME2,
    @UserId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  u.UserId,
            u.FullName                               AS StaffName,
            COUNT(o.OrderId)                         AS OrderCount,
            ISNULL(SUM(o.TotalAmount), 0)            AS Revenue,
            ISNULL(SUM(o.Discount + o.PointsDiscount), 0) AS TotalDiscount,
            MIN(o.CreatedAt)                         AS FirstOrderAt,
            MAX(o.CreatedAt)                         AS LastOrderAt
    FROM    Orders o
    JOIN    Users  u ON u.UserId = o.UserId
    WHERE   o.Status = N'Completed'
      AND   o.CreatedAt >= @From AND o.CreatedAt < @To
      AND   (@UserId IS NULL OR o.UserId = @UserId)
    GROUP BY u.UserId, u.FullName
    ORDER BY Revenue DESC;
END");

            // Doanh thu từng ngày cho biểu đồ cột
            migrationBuilder.Sql(@"
CREATE OR ALTER PROCEDURE dbo.sp_DailyRevenue
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  CAST(CreatedAt AS DATE)        AS [Date],
            COUNT(*)                       AS OrderCount,
            ISNULL(SUM(TotalAmount), 0)    AS Revenue
    FROM    Orders
    WHERE   Status = N'Completed'
      AND   CreatedAt >= @FromDate
      AND   CreatedAt < DATEADD(DAY, 1, CAST(@ToDate AS DATETIME2))
    GROUP BY CAST(CreatedAt AS DATE)
    ORDER BY [Date];
END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.sp_DailyRevenue");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.sp_RevenueByShift");
        }
    }
}
