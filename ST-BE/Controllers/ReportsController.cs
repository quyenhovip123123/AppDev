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
        private readonly AppDbContext _db;

        public ReportsController(AppDbContext db)
        {
            _db = db;
        }

        // GET /api/reports/dashboard?from=2026-10-01&to=2026-10-31&lowStockThreshold=10
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int lowStockThreshold = 10)
        {
            var fromDate = (from ?? DateTime.Today.AddDays(-6)).Date;
            var toDate = (to ?? DateTime.Today).Date;
            if (fromDate > toDate) return BadRequest(new { message = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày" });
            var toExclusive = toDate.AddDays(1);

            // Tổng hợp trên bộ nhớ (SQLite không hỗ trợ SUM trên kiểu decimal)
            var orders = await _db.Orders.AsNoTracking().Include(o => o.Details)
                .Where(o => o.Status == OrderStatus.Completed && o.CreatedAt >= fromDate && o.CreatedAt < toExclusive)
                .ToListAsync();

            var importTotals = await _db.ImportReceipts.AsNoTracking()
                .Where(r => r.CreatedAt >= fromDate && r.CreatedAt < toExclusive)
                .Select(r => r.TotalAmount).ToListAsync();

            var details = orders.SelectMany(o => o.Details).ToList();
            var productCodes = await _db.Products.AsNoTracking().ToDictionaryAsync(p => p.ProductId, p => p.ProductCode);

            var daily = Enumerable.Range(0, (toDate - fromDate).Days + 1)
                .Select(i => fromDate.AddDays(i))
                .Select(day =>
                {
                    var dayOrders = orders.Where(o => o.CreatedAt.Date == day).ToList();
                    return new DailyRevenueDto { Date = day, OrderCount = dayOrders.Count, Revenue = dayOrders.Sum(o => o.TotalAmount) };
                }).ToList();

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

            var revenue = orders.Sum(o => o.TotalAmount);
            var cost = details.Sum(d => d.UnitCost * d.Quantity);

            return Ok(new DashboardDto
            {
                From = fromDate,
                To = toDate,
                OrderCount = orders.Count,
                Revenue = revenue,
                Profit = revenue - cost,
                ImportTotal = importTotals.Sum(),
                ProductCount = await _db.Products.CountAsync(p => p.IsActive),
                CustomerCount = await _db.Customers.CountAsync(),
                DailyRevenue = daily,
                TopProducts = details.GroupBy(d => d.ProductId)
                    .Select(g => new TopProductDto
                    {
                        ProductCode = productCodes.GetValueOrDefault(g.Key, string.Empty),
                        ProductName = g.First().ProductName,
                        QuantitySold = g.Sum(d => d.Quantity),
                        Revenue = g.Sum(d => d.LineTotal)
                    })
                    .OrderByDescending(t => t.QuantitySold).Take(10).ToList(),
                LowStockProducts = lowStock
            });
        }
    }
}
