using PathFinder.SharedKernel.Constants;
using PathFinder.SharedKernel.Enums;
using PathFinder.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace PathFinder.SharedKernel.Filters
{
    public class LanguageActionAttribute : Attribute, IActionFilter
    {
        public static ILoggerService? _loggerService { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _loggerService = context.HttpContext.RequestServices.GetService<ILoggerService>();
            if (context.Exception != null) 
                _loggerService.Log(AppConstants.FileActions, context.HttpContext.Request.Path, context.Exception.Message);
            else
            {
                ResponseStatus responseStatus = (ResponseStatus)context.HttpContext.Response.StatusCode;
                _loggerService.Log(AppConstants.FileActions, context.HttpContext.Request.Path, (int)responseStatus + " " + responseStatus.ToString());
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
