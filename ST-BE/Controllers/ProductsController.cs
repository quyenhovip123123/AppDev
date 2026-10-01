using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;
using ST_BE.Services;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        // GET /api/products?keyword=bút&categoryId=1&includeInactive=false
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword, [FromQuery] int? categoryId, [FromQuery] bool includeInactive = false)
        {
            var query = Query();
            if (!includeInactive) query = query.Where(p => p.IsActive);
            if (categoryId > 0) query = query.Where(p => p.CategoryId == categoryId);

            var list = await query.ToListAsync();

            // Lọc từ khóa trên bộ nhớ để so khớp tiếng Việt có dấu không phân biệt hoa/thường
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();
                list = list.Where(p => p.ProductName.Contains(k, StringComparison.OrdinalIgnoreCase)
                                    || p.ProductCode.Contains(k, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            list.ForEach(HideCostForStaff);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await Query().FirstOrDefaultAsync(x => x.ProductId == id);
            if (p == null) return NotFound(new { message = "Không tìm thấy mặt hàng" });
            HideCostForStaff(p);
            return Ok(p);
        }

        // GET /api/products/by-code/BUT001 — dùng khi bán hàng quét/nhập mã
        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var p = await Query().FirstOrDefaultAsync(x => x.ProductCode == code.Trim().ToUpper() && x.IsActive);
            if (p == null) return NotFound(new { message = $"Không tìm thấy mặt hàng có mã {code}" });
            HideCostForStaff(p);
            return Ok(p);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var code = request.ProductCode.Trim().ToUpper();
            if (await _db.Products.AnyAsync(p => p.ProductCode == code))
                return Conflict(new { message = "Mã hàng đã tồn tại" });
            if (!await _db.Categories.AnyAsync(c => c.CategoryId == request.CategoryId))
                return BadRequest(new { message = "Nhóm hàng không tồn tại" });

            var product = new Product();
            Apply(product, request);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, await Query().FirstAsync(x => x.ProductId == product.ProductId));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest request)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound(new { message = "Không tìm thấy mặt hàng" });

            var code = request.ProductCode.Trim().ToUpper();
            if (await _db.Products.AnyAsync(p => p.ProductCode == code && p.ProductId != id))
                return Conflict(new { message = "Mã hàng đã tồn tại" });
            if (!await _db.Categories.AnyAsync(c => c.CategoryId == request.CategoryId))
                return BadRequest(new { message = "Nhóm hàng không tồn tại" });

            Apply(product, request);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // Mặt hàng đã phát sinh giao dịch thì chỉ chuyển sang "Ngừng kinh doanh" để giữ lịch sử
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound(new { message = "Không tìm thấy mặt hàng" });

            var used = await _db.OrderDetails.AnyAsync(d => d.ProductId == id)
                    || await _db.ImportReceiptDetails.AnyAsync(d => d.ProductId == id);
            if (used)
            {
                product.IsActive = false;
                await _db.SaveChangesAsync();
                return Ok(new { message = "Mặt hàng đã có giao dịch nên được chuyển sang trạng thái Ngừng kinh doanh" });
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Đã xóa mặt hàng" });
        }

        // Giá nhập là thông tin kinh doanh nhạy cảm: chỉ Admin được xem
        private void HideCostForStaff(ProductDto p)
        {
            if (!User.IsAdmin()) p.CostPrice = 0;
        }

        private static void Apply(Product p, ProductRequest r)
        {
            p.ProductCode = r.ProductCode.Trim().ToUpper();
            p.ProductName = r.ProductName.Trim();
            p.CategoryId = r.CategoryId;
            p.Unit = r.Unit.Trim();
            p.CostPrice = r.CostPrice;
            p.SalePrice = r.SalePrice;
            p.StockQuantity = r.StockQuantity;
            p.Description = r.Description?.Trim();
            p.IsActive = r.IsActive;
        }

        private IQueryable<ProductDto> Query() =>
            _db.Products.AsNoTracking().OrderBy(p => p.ProductCode).Select(p => new ProductDto
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
                Description = p.Description,
                IsActive = p.IsActive
            });
    }
}
