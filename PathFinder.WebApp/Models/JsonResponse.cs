
using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Extensions;
using PathFinder.DataTransferObjects.Resources;

namespace PathFinder.WebApp.Models
{
    public class JsonResponse<T> : JsonResponse where T : class
    {
        public T Value { get; set; }
    }
    public class JsonResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string Html { get; set; }

        public async Task<IActionResult> GetJsonResultAsync<TModel>(Controller controller, string partialPage, TModel model)
        {
            try
            {
                Html = await controller.RenderPartialViewWithModelAsStringAsync(partialPage, model);
                Message = CoreResources.InputError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return controller.Json(this);
        }
        public async Task SetHtmlToPartialView<TModel>(Controller controller, string partialPage, TModel model, string message,bool isSuccess=false)
        {
            try
            {
                IsSuccess = isSuccess;
                Html = await controller.RenderPartialViewWithModelAsStringAsync(partialPage, model);
                Message = message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
