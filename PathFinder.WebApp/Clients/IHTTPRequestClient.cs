using PathFinder.WebApp.Models;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.SharedKernel.Enums;

namespace PathFinder.WebApp.Clients
{
    public interface IHttpRequestClient
    {
        public Task<HttpResponse<TResult>> GetAsync<TResult>(string URL);
        public Task<HttpResponse<APIResult>> PostAsync<TResult, Body>(string URL, Body body);
        public Task<HttpResponse<APIResult>> PostAsync<Body>(string URL, Body body);
        public Task<HttpResponse<TResult>> PutAsync<TResult, Body>(string URL, Body body);
        public Task<HttpResponse<Body>> PutAsync<Body>(string URL, Body body);
        public Task<HttpResponse<TResult>> DeleteAsync<TResult>(string URL);
    }
}
