using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;

namespace ST_BE.Controllers
{
    // Báo cáo thống kê — chỉ Admin
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class ReportsController : ControllerBase
    {
        private const int LowStockThreshold = 10;
        private readonly AppDbContext _db;
        private readonly AdoNetRepository _ado;

        public ReportsController(AppDbContext db, AdoNetRepository ado)
        {
            _db = db;
            _ado = ado;
        }

        // GET /api/reports/dashboard-summary — 4 thẻ KPI (ADO.NET ExecuteScalar)
        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> DashboardSummary()
        {
            return Ok(await _ado.GetDashboardSummaryAsync(LowStockThreshold));
        }

        // GET /api/reports/daily-revenue?from=2026-10-01&to=2026-10-31 — gọi Stored Procedure sp_DailyRevenue
        [HttpGet("daily-revenue")]
        public async Task<IActionResult> DailyRevenue([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var (fromDate, toDate) = ((from ?? DateTime.Today.AddDays(-6)).Date, (to ?? DateTime.Today).Date);
            if (fromDate > toDate) return BadRequest(new { message = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày" });
            return Ok(await GetDailyRevenueAsync(fromDate, toDate));
        }

        // GET /api/reports/shift-revenue?from=2026-10-17T07:00&to=2026-10-17T15:00&userId=2
        // Doanh thu ca làm việc — gọi Stored Procedure sp_RevenueByShift
        [HttpGet("shift-revenue")]
        public async Task<IActionResult> ShiftRevenue([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? userId)
        {
            var fromTime = from ?? DateTime.Today;
            var toTime = to ?? DateTime.Now;
            if (fromTime >= toTime) return BadRequest(new { message = "Thời điểm bắt đầu ca phải trước thời điểm kết thúc" });
            return Ok(await _ado.GetShiftRevenueAsync(fromTime, toTime, userId));
        }

        // GET /api/reports/top-products?from=&to=&top=5 — mặt hàng bán chạy (LINQ GroupBy)
        [HttpGet("top-products")]
        public async Task<IActionResult> TopProducts([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int top = 5)
        {
            var fromDate = (from ?? DateTime.Today.AddDays(-29)).Date;
            var toExclusive = (to ?? DateTime.Today).Date.AddDays(1);
            return Ok(await GetTopProductsAsync(fromDate, toExclusive, Math.Clamp(top, 1, 50)));
        }

        // GET /api/reports/dashboard?from=2026-10-01&to=2026-10-31&lowStockThreshold=10
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int lowStockThreshold = LowStockThreshold)
        {
            var fromDate = (from ?? DateTime.Today.AddDays(-6)).Date;
            var toDate = (to ?? DateTime.Today).Date;
            if (fromDate > toDate) return BadRequest(new { message = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày" });
            var toExclusive = toDate.AddDays(1);

            var orders = _db.Orders.AsNoTracking()
                .Where(o => o.Status == OrderStatus.Completed && o.CreatedAt >= fromDate && o.CreatedAt < toExclusive);

            var revenue = await orders.SumAsync(o => o.TotalAmount);
            var cost = await orders.SelectMany(o => o.Details).SumAsync(d => d.UnitCost * d.Quantity);

            var lowStock = await _db.Products.AsNoTracking()
                .Where(p => p.IsActive && p.StockQuantity <= lowStockThreshold)
                .OrderBy(p => p.StockQuantity)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductCode = p.ProductCode,
                    ProductName = p.ProductName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category!.CategoryName,
                    Unit = p.Unit,
                    CostPrice = p.CostPrice,
                    SalePrice = p.SalePrice,
                    StockQuantity = p.StockQuantity,
                    IsActive = p.IsActive
                }).ToListAsync();

            return Ok(new DashboardDto
            {
                From = fromDate,
                To = toDate,
                OrderCount = await orders.CountAsync(),
                Revenue = revenue,
                Profit = revenue - cost,
                ImportTotal = await _db.ImportReceipts.AsNoTracking()
                    .Where(r => r.CreatedAt >= fromDate && r.CreatedAt < toExclusive)
                    .SumAsync(r => r.TotalAmount),
                ProductCount = await _db.Products.CountAsync(p => p.IsActive),
                CustomerCount = await _db.Customers.CountAsync(),
                DailyRevenue = await GetDailyRevenueAsync(fromDate, toDate),
                TopProducts = await GetTopProductsAsync(fromDate, toExclusive, 5),
                LowStockProducts = lowStock
            });
        }

        // Kết quả SP chỉ có những ngày có bán -> bổ sung các ngày trống = 0 để biểu đồ liền mạch
        private async Task<List<DailyRevenueDto>> GetDailyRevenueAsync(DateTime fromDate, DateTime toDate)
        {
            var rows = (await _ado.GetDailyRevenueAsync(fromDate, toDate)).ToDictionary(r => r.Date.Date);
            var start = DateTime.SpecifyKind(fromDate, DateTimeKind.Unspecified);   // cùng kiểu ngày với kết quả từ SQL
            return Enumerable.Range(0, (toDate - fromDate).Days + 1)
                .Select(i => start.AddDays(i))
                .Select(day => rows.GetValueOrDefault(day) ?? new DailyRevenueDto { Date = day })
                .ToList();
        }

        // Top N bán chạy: GroupBy(ProductId) -> OrderByDescending(Sum Quantity) -> Take(N), chạy trên SQL Server
        private Task<List<TopProductDto>> GetTopProductsAsync(DateTime fromDate, DateTime toExclusive, int top) =>
            _db.OrderDetails.AsNoTracking()
                .Where(d => d.Order!.Status == OrderStatus.Completed && d.Order.CreatedAt >= fromDate && d.Order.CreatedAt < toExclusive)
                .GroupBy(d => d.ProductId)
                .Select(g => new TopProductDto
                {
                    ProductCode = g.Max(d => d.Product!.ProductCode)!,
                    ProductName = g.Max(d => d.ProductName)!,
                    QuantitySold = g.Sum(d => d.Quantity),
                    Revenue = g.Sum(d => d.LineTotal)
                })
                .OrderByDescending(t => t.QuantitySold)
                .Take(top)
                .ToListAsync();
    }
}
