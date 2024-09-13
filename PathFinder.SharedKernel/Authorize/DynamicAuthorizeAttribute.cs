using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PathFinder.SharedKernel.Polices.Mapping;

namespace PathFinder.SharedKernel.Authorize
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class DynamicAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string controllerName = "";
            string actionName = "";
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                controllerName = controllerActionDescriptor.ControllerName;
                actionName = controllerActionDescriptor.ActionName;

            }

            var updatedPolicy = GenericPermission.GetDynamicPolicy(controllerName, actionName);

            if (!string.IsNullOrEmpty(updatedPolicy))
            {
                var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
                var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, updatedPolicy);
                if (!authorizationResult.Succeeded)
                    context.Result = new ForbidResult();
            }
        }
    }
}
