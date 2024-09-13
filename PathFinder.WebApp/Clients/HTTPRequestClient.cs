using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Models;
using PathFinder.WebApp.Services;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.SharedKernel.Enums;
using PathFinder.SharedKernel.Extensions;
using Microsoft.Extensions.Localization;

namespace PathFinder.WebApp.Web
{
    public class HttpRequestClient : IHttpRequestClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _context;
        private readonly IStringLocalizer<ModelValidationResources> _localizer;
        public HttpRequestClient(HttpClient httpClient, IConfiguration config, IStringLocalizer<ModelValidationResources> localizer, IHttpContextAccessor context)
        {
            _configuration = config;
            _localizer = localizer;
            _context = context;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _context.GetToken());
        }
        public async Task<HttpResponse<APIResult>> GetAsync<APIResult>(string url)
        {
            try
            {
                HttpResponseMessage response;
                _httpClient.DefaultRequestHeaders.Add("Accept-Language", Thread.CurrentThread.CurrentCulture.Name);

                 response = await _httpClient.GetAsync(BuildUrl(url));

                return await new HttpResponseResultService<APIResult>(_localizer).GetHttpResponse(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponse<APIResult>> PostAsync<TResult, Body>(string url, Body body)
        {
            try
            {
                HttpResponseMessage response;
                _httpClient.DefaultRequestHeaders.Add("Accept-Language", Thread.CurrentThread.CurrentCulture.Name);

                 response = await _httpClient.PostAsJsonAsync(BuildUrl(url), body);

                return await new HttpResponseResultService<APIResult>(_localizer).GetHttpResponse(response);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<HttpResponse<APIResult>> PostAsync<Body>(string url, Body body)
        {
            return await PostAsync<Body, Body>(url, body);
        }
        public async Task<HttpResponse<TResult>> PutAsync<TResult, Body>(string url, Body body)
        {
            HttpResponseMessage response;
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", Thread.CurrentThread.CurrentCulture.Name);

             response = await _httpClient.PutAsJsonAsync(BuildUrl(url), body);

            return await new HttpResponseResultService<TResult>(_localizer).GetHttpResponse(response);
        }
        public async Task<HttpResponse<Body>> PutAsync<Body>(string url, Body body)
        {
            return await PutAsync<Body, Body>(url, body);
        }
        public async Task<HttpResponse<TResult>> DeleteAsync<TResult>(string url)
        {
            HttpResponseMessage response;
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", Thread.CurrentThread.CurrentCulture.Name);

             response = await _httpClient.DeleteAsync(BuildUrl(url));

            return await new HttpResponseResultService<TResult>(_localizer).GetHttpResponse(response);
        }



        private string BuildUrl(string url) => $"{_configuration["httpScheme"]}://{_configuration["domainForApi"]}:{_configuration["apiHostPortNumber"]}/{url}";
    }
}