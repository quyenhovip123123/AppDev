using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;
using ST_BE.Services;

namespace ST_BE.Controllers
{
    // Phiếu nhập kho — chỉ Admin
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class ImportsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ImportsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var query = Query();
            if (from.HasValue) query = query.Where(r => r.CreatedAt >= from.Value.Date);
            if (to.HasValue) query = query.Where(r => r.CreatedAt < to.Value.Date.AddDays(1));
            var list = await query.OrderByDescending(r => r.CreatedAt).ToListAsync();
            return Ok(list.Select(ToDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await Query().FirstOrDefaultAsync(x => x.ImportReceiptId == id);
            if (r == null) return NotFound(new { message = "Không tìm thấy phiếu nhập" });
            return Ok(ToDto(r));
        }

        // POST /api/imports — nhập hàng: cộng tồn kho, cập nhật giá nhập mới
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateImportRequest request)
        {
            var ids = request.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _db.Products.Where(p => ids.Contains(p.ProductId)).ToDictionaryAsync(p => p.ProductId);

            await using var tx = await _db.Database.BeginTransactionAsync();

            var receipt = new ImportReceipt
            {
                ReceiptCode = $"PN{DateTime.Now:yyMMddHHmmssfff}",
                CreatedAt = DateTime.Now,
                UserId = User.GetUserId(),
                SupplierName = request.SupplierName.Trim(),
                Note = request.Note?.Trim()
            };

            foreach (var item in request.Items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                    return BadRequest(new { message = $"Mặt hàng (ID {item.ProductId}) không tồn tại" });

                product.StockQuantity += item.Quantity;
                product.CostPrice = item.UnitCost;
                receipt.Details.Add(new ImportReceiptDetail
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitCost = item.UnitCost,
                    LineTotal = item.UnitCost * item.Quantity
                });
            }
            receipt.TotalAmount = receipt.Details.Sum(d => d.LineTotal);

            _db.ImportReceipts.Add(receipt);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            var created = await Query().FirstAsync(r => r.ImportReceiptId == receipt.ImportReceiptId);
            return CreatedAtAction(nameof(GetById), new { id = receipt.ImportReceiptId }, ToDto(created));
        }

        private IQueryable<ImportReceipt> Query() =>
            _db.ImportReceipts.AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.Details).ThenInclude(d => d.Product);

        private static ImportReceiptDto ToDto(ImportReceipt r) => new()
        {
            ImportReceiptId = r.ImportReceiptId,
            ReceiptCode = r.ReceiptCode,
            CreatedAt = r.CreatedAt,
            StaffName = r.User?.FullName ?? string.Empty,
            SupplierName = r.SupplierName,
            Note = r.Note,
            TotalAmount = r.TotalAmount,
            Details = r.Details.Select(d => new ImportDetailDto
            {
                ProductId = d.ProductId,
                ProductCode = d.Product?.ProductCode ?? string.Empty,
                ProductName = d.Product?.ProductName ?? string.Empty,
                Quantity = d.Quantity,
                UnitCost = d.UnitCost,
                LineTotal = d.LineTotal
            }).ToList()
        };
    }
}
