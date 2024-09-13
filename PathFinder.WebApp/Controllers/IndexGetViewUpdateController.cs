using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Models;
using PathFinder.Common.Helpers.Models;
using PathFinder.DataTransferObjects.Resources;

namespace PathFinder.WebApp.Controllers
{
    public class IndexGetViewUpdateController<GetDTO, ViewDTO, UpdateDTO> : ViewUpdateController<GetDTO, ViewDTO, UpdateDTO>
        where GetDTO : class
        where ViewDTO : class
        where UpdateDTO : class
    {
        
        public IndexGetViewUpdateController(IHttpRequestClient httpClient, string baseURL, string viewPartialView, string updatePartialView) : base(httpClient, baseURL, viewPartialView, updatePartialView)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
