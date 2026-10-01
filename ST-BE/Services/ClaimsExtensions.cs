using ST_BE.Models;
using System.Security.Claims;

namespace ST_BE.Services
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user) =>
            int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;

        public static bool IsAdmin(this ClaimsPrincipal user) => user.IsInRole(Roles.Admin);
    }
}
