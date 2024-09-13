using PathFinder.Common.Helpers.Enums;
using PathFinder.SharedKernel.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;

namespace PathFinder.SharedKernel.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string? GetEmail(this IHttpContextAccessor context) =>
            context.HttpContext?.User?.FindFirst(ClaimConstants.Email)?.Value;
        public static string? GetPhone(this IHttpContextAccessor context) =>
            context.HttpContext?.User?.FindFirst(ClaimConstants.PhoneNumber)?.Value;
        public static string? GetUserName(this IHttpContextAccessor context) =>
            context.HttpContext?.User?.FindFirst(ClaimConstants.UserId)?.Value;
        //public static string? GetUserId(this IHttpContextAccessor context) =>
        //    context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public static string? GetUserEmail(this IHttpContextAccessor context) =>
            context.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        public static string? GetRoleName(this IHttpContextAccessor context) =>
            context.HttpContext?.User?.FindFirst(ClaimConstants.Role)?.Value;
        public static int GetUserId(this IHttpContextAccessor context)
        {
            string user = context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId= user != null ? int.Parse(user) : 0;
            return userId;
        }

        public static int GetRole(this IHttpContextAccessor context)
        {
            string role = context.HttpContext?.User?.FindFirst(ClaimConstants.Role)?.Value;
            int roleId = (int)((RoleType)Enum.Parse(typeof(RoleType), role));
            return roleId;
        }
        public static bool IsAdmin(this IHttpContextAccessor context)
        {
            var role = context.HttpContext?.User?.FindFirst(ClaimConstants.Role)?.Value;
            return AppConstants.Admin == role;
        }


        public static int GetTokenSize(this IHttpContextAccessor context)
        {
            int size = Convert.ToInt32(context.HttpContext.Request.Cookies["tokenSize"]);
            return size;
        }
        public static string? GetToken(this IHttpContextAccessor context)
        {
            string token = context.HttpContext.Session.GetString("Token");
            return string.IsNullOrEmpty(token)? null : token;
        }
    }
}
