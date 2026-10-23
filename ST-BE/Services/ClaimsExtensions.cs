using ST_BE.Models;
using System.Security.Claims;

namespace ST_BE.Services
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user) =>
            int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;

        public static bool IsAdmin(this ClaimsPrincipal user) => user.IsInRole(Roles.Admin);

        // Quản lý và nhân viên kiểm kho được xem giá nhập (cần cho nhập kho / kiểm kê)
        public static bool CanSeeCostPrice(this ClaimsPrincipal user) => user.IsInRole(Roles.Admin) || user.IsInRole(Roles.Warehouse);
    }
}
