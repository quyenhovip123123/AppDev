using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;

namespace ST_BE.Controllers
{
    // Xem: mọi tài khoản đã đăng nhập. Thêm/sửa/xóa: chỉ Admin
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Query().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await Query().FirstOrDefaultAsync(c => c.CategoryId == id);
            if (cat == null) return NotFound(new { message = "Không tìm thấy nhóm hàng!" });
            return Ok(cat);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });

            var all = await Query().ToListAsync();
            var result = all.Where(c => c.CategoryName.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request)
        {
            var cat = new Category { CategoryName = request.CategoryName.Trim(), Description = request.Description?.Trim() };
            _db.Categories.Add(cat);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cat.CategoryId },
                new CategoryDto { CategoryId = cat.CategoryId, CategoryName = cat.CategoryName, Description = cat.Description });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null) return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });

            cat.CategoryName = request.CategoryName.Trim();
            cat.Description = request.Description?.Trim();
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null) return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });

            if (await _db.Products.AnyAsync(p => p.CategoryId == id))
                return BadRequest(new { message = "Nhóm hàng đang có sản phẩm, không thể xóa!" });

            _db.Categories.Remove(cat);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private IQueryable<CategoryDto> Query() =>
            _db.Categories.AsNoTracking().OrderBy(c => c.CategoryId).Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ProductCount = c.Products.Count
            });
    }
}
