using Microsoft.Extensions.Localization;
using PathFinder.WebApp.Models;
using PathFinder.DataTransferObjects.Resources;
using System.Net;

namespace PathFinder.WebApp.Services
{
    public interface IHttpResponseResultService<TResult>
    {
        Task<HttpResponse<TResult>> GetHttpResponse(HttpResponseMessage response);
    }
    public class HttpResponseResultService<TResult>: IHttpResponseResultService<TResult>
    {
        private readonly IStringLocalizer<ModelValidationResources> _localizer;
        public HttpResponseResultService(IStringLocalizer<ModelValidationResources> localizer)
        {
            _localizer = localizer;
        }
        public async Task<HttpResponse<TResult>> GetHttpResponse(HttpResponseMessage response)
        {
            try
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return new HttpResponse<TResult>() { IsSuccess = response.IsSuccessStatusCode, Message = CoreResources.SuccessMessage, Value = await response.Content.ReadFromJsonAsync<TResult>() };
                    case HttpStatusCode.NotFound:
                        return new HttpResponse<TResult>() { IsSuccess = response.IsSuccessStatusCode, Message = string.IsNullOrEmpty(response.ReasonPhrase)? CoreResources.NotFound : _localizer[response.ReasonPhrase] };
                    case HttpStatusCode.InternalServerError:
                        return new HttpResponse<TResult>() { IsSuccess = response.IsSuccessStatusCode, Message = _localizer[response.ReasonPhrase] };
                    case HttpStatusCode.Unauthorized:
                        return new HttpResponse<TResult>() { IsSuccess = response.IsSuccessStatusCode, Message = _localizer[response.ReasonPhrase] };

                    default:
                        return new HttpResponse<TResult>() { IsSuccess = response.IsSuccessStatusCode, Message = response.IsSuccessStatusCode ? CoreResources.SuccessMessage : _localizer[response.ReasonPhrase] };
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
