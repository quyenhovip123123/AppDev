using Microsoft.EntityFrameworkCore;
using ST_BE.Dtos;

namespace ST_BE.Data
{
    // Phân trang phía máy chủ dùng chung cho các API GET .../paged?page=1&pageSize=20
    public static class Paging
    {
        public const int MaxPageSize = 100;

        public static bool IsValid(int page, int pageSize) => page >= 1 && pageSize >= 1 && pageSize <= MaxPageSize;

        public static object InvalidMessage => new { message = $"page phải >= 1 và pageSize trong khoảng 1-{MaxPageSize}" };

        // COUNT(*) toàn bộ kết quả lọc + OFFSET/FETCH lấy đúng 1 trang (query phải đã được OrderBy)
        public static async Task<PagedResult<TDto>> ToPagedAsync<T, TDto>(this IQueryable<T> query, int page, int pageSize, Func<T, TDto> map)
        {
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResult<TDto> { Items = items.Select(map).ToList(), Page = page, PageSize = pageSize, TotalCount = total };
        }
    }
}
