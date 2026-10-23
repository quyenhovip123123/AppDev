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
        private const int MaxPageSize = 100;
        private readonly AppDbContext _db;
        private readonly AdoNetRepository _ado;

        public ProductsController(AppDbContext db, AdoNetRepository ado)
        {
            _db = db;
            _ado = ado;
        }

        // GET /api/products?keyword=bút&categoryId=1&includeInactive=false — toàn bộ danh sách (màn hình bán hàng dùng)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword, [FromQuery] int? categoryId, [FromQuery] bool includeInactive = false)
        {
            var query = Query();
            if (!includeInactive) query = query.Where(p => p.IsActive);
            if (categoryId > 0) query = query.Where(p => p.CategoryId == categoryId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();
                query = query.Where(p => p.ProductName.Contains(k) || p.ProductCode.Contains(k) || p.Barcode == k);
            }

            var list = await query.ToListAsync();
            list.ForEach(HideCostForStaff);
            return Ok(list);
        }

        // GET /api/products/paged?keyword=bút&categoryId=1&page=1&pageSize=20 — phân trang phía máy chủ
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? keyword, [FromQuery] int? categoryId,
            [FromQuery] bool includeInactive = false, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (page < 1 || pageSize < 1 || pageSize > MaxPageSize)
                return BadRequest(new { message = $"page phải >= 1 và pageSize trong khoảng 1-{MaxPageSize}" });

            // Include: nạp kèm nhóm hàng để lấy CategoryName (không bị null)
            // AsNoTracking: truy vấn chỉ đọc, EF không cần theo dõi thay đổi -> nhẹ RAM
            IQueryable<Product> query = _db.Products.AsNoTracking().Include(p => p.Category);

            if (!includeInactive) query = query.Where(p => p.IsActive);
            if (categoryId > 0) query = query.Where(p => p.CategoryId == categoryId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();
                // .Contains() được dịch thành LIKE N'%...%' trên SQL Server (collation không phân biệt hoa/thường)
                query = query.Where(p => p.ProductName.Contains(k) || p.ProductCode.Contains(k) || p.Barcode == k);
            }

            var total = await query.CountAsync();
            var products = await query.OrderBy(p => p.ProductCode)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductCode = p.ProductCode,
                Barcode = p.Barcode,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.CategoryName ?? string.Empty,
                Unit = p.Unit,
                CostPrice = p.CostPrice,
                SalePrice = p.SalePrice,
                StockQuantity = p.StockQuantity,
                Description = p.Description,
                IsActive = p.IsActive
            }).ToList();
            items.ForEach(HideCostForStaff);

            return Ok(new PagedResult<ProductDto> { Items = items, Page = page, PageSize = pageSize, TotalCount = total });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await Query().FirstOrDefaultAsync(x => x.ProductId == id);
            if (p == null) return NotFound(new { message = "Không tìm thấy mặt hàng" });
            HideCostForStaff(p);
            return Ok(p);
        }

        // GET /api/products/by-barcode/8938686000019 hoặc /api/products/by-code/BUT001 — quầy bán hàng quét mã.
        // Khớp theo mã vạch EAN hoặc mã hàng nội bộ. Tra cứu bằng ADO.NET (SqlDataReader) cho nhanh
        [HttpGet("by-code/{code}")]
        [HttpGet("by-barcode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var p = await _ado.GetProductByCodeAsync(code);
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
            var barcode = NormalizeBarcode(request.Barcode);
            if (barcode != null && await _db.Products.AnyAsync(p => p.Barcode == barcode))
                return Conflict(new { message = "Mã vạch đã được gán cho mặt hàng khác" });
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
            var barcode = NormalizeBarcode(request.Barcode);
            if (barcode != null && await _db.Products.AnyAsync(p => p.Barcode == barcode && p.ProductId != id))
                return Conflict(new { message = "Mã vạch đã được gán cho mặt hàng khác" });
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

        // Chuỗi rỗng -> null để chỉ mục UNIQUE (lọc IS NOT NULL) cho phép nhiều mặt hàng chưa có mã vạch
        private static string? NormalizeBarcode(string? barcode) =>
            string.IsNullOrWhiteSpace(barcode) ? null : barcode.Trim();

        private static void Apply(Product p, ProductRequest r)
        {
            p.ProductCode = r.ProductCode.Trim().ToUpper();
            p.Barcode = NormalizeBarcode(r.Barcode);
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
                Barcode = p.Barcode,
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
