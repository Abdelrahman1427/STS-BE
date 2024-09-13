using PathFinder.DataTransferObjects.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PathFinder.WebApp.Models
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private static string? _Token;
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            _Token = context.HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(_Token))
            {
                var jwtToken = new JwtHelpers().DecodeJWTToken(_Token);
                if (jwtToken != null)
                {
                    context.HttpContext.Response.Headers.Add("authToken", _Token);
                    context.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                    context.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                    return;
                }
            }
            context.Result = new RedirectToActionResult("Login", "Account", new { area = "User" });
        }
    }
}
