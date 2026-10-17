using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Models;
using System.Threading.Tasks;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DBContext dbContext;
        public CategoriesController(DBContext context)
        {
            dbContext = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dbContext.Categories.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng trong CSDL!" });
            }
            return Ok(category);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });
            }
            var result = await dbContext.Categories
                .Where(c => c.CategoryName.Contains(keyword))
                .ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category newCat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.Categories.Add(newCat);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newCat.CategoryId }, newCat);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updateCat)
        {
            var cat = await dbContext.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });
            }

            cat.CategoryName = updateCat.CategoryName;
            cat.Description = updateCat.Description;

            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await dbContext.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });
            }

            dbContext.Categories.Remove(cat);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
