using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Models;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DBContext _context;

        public ProductsController(DBContext context)
        {
            _context = context;
        }

        // Chuyển Entity sang dạng phẳng kèm tên nhóm hàng cho Client hiển thị
        private IQueryable<object> ProjectProducts(IQueryable<Product> query)
        {
            return query.Select(p => new
            {
                p.ProductId,
                p.Barcode,
                p.ProductName,
                p.Price,
                p.StockQuantity,
                p.CategoryId,
                CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
            });
        }

        // GET /api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await ProjectProducts(_context.Products.AsNoTracking().OrderBy(p => p.ProductId)).ToListAsync();
            return Ok(products);
        }

        // GET /api/products/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await ProjectProducts(_context.Products.AsNoTracking().Where(p => p.ProductId == id)).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }
            return Ok(product);
        }

        // GET /api/products/barcode/{barcode}  (dùng cho màn hình POS quét mã vạch)
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await ProjectProducts(_context.Products.AsNoTracking().Where(p => p.Barcode == barcode)).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm có mã vạch này!" });
            }
            return Ok(product);
        }

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Products.AnyAsync(p => p.Barcode == newProduct.Barcode))
            {
                return Conflict(new { message = "Mã vạch đã tồn tại!" });
            }

            if (!await _context.Categories.AnyAsync(c => c.CategoryId == newProduct.CategoryId))
            {
                return BadRequest(new { message = "Nhóm hàng không tồn tại!" });
            }

            newProduct.ProductId = 0;
            newProduct.Category = null;
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductId }, newProduct);
        }

        // PUT /api/products/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updateProduct)
        {
            if (id != updateProduct.ProductId)
            {
                return BadRequest(new { message = "ID không khớp!" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }

            if (await _context.Products.AnyAsync(p => p.Barcode == updateProduct.Barcode && p.ProductId != id))
            {
                return Conflict(new { message = "Mã vạch đã được dùng cho sản phẩm khác!" });
            }

            product.Barcode = updateProduct.Barcode;
            product.ProductName = updateProduct.ProductName;
            product.Price = updateProduct.Price;
            product.StockQuantity = updateProduct.StockQuantity;
            product.CategoryId = updateProduct.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            }

            if (await _context.OrderDetails.AnyAsync(d => d.ProductId == id))
            {
                return BadRequest(new { message = "Sản phẩm đã phát sinh hóa đơn, không thể xóa!" });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
