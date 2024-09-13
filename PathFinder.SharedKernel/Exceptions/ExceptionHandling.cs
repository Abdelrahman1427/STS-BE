using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using STS.SharedKernel.Constants;
using STS.SharedKernel.Interfaces;
using STS.DataTransferObjects.Resources;


namespace STS.SharedKernel.Exceptions
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
    public class ExceptionHandling 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandling> _logger;
        private readonly ILoggerService _loggerService;
        private readonly IStringLocalizer<ModelValidationResources> _localizer;
        public ExceptionHandling
            (
                RequestDelegate next,
                ILogger<ExceptionHandling> logger,
                ILoggerService loggerService,
                IStringLocalizer<ModelValidationResources> localizer
                )
        {
            _next = next;
            _logger = logger;
            _loggerService = loggerService;
            _localizer = localizer;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse();
            switch (exception)
            {
                case ApplicationException:
                    if (exception.Message.Contains("Invalid token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = _localizer[exception.Message];
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = _localizer[exception.Message];
                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    string exceptionMessage = _localizer[exception.Message];
                    errorResponse.Message = exceptionMessage;
                    break;

                case STSException:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = _localizer[exception.Message];
                    break;

                case ForbiddenException:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorResponse.Message = _localizer[exception.Message];
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server Error";
                    break;
            }
            await _loggerService.Log(AppConstants.FileExceptions, context.Request.Path, exception.ToString());
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = errorResponse.Message;
            await context.Response.WriteAsync(result);
        }
    }
}