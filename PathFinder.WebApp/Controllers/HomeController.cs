using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Models;
using System.Diagnostics;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.SharedKernel.Enums;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Areas.DataEntry;

namespace PathFinder.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpRequestClient _httpClient;

        public HomeController(
            ILogger<HomeController> logger, IHttpRequestClient httpClient
            )
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var checkAuthorize = await _httpClient.GetAsync<APIResult>(DataEntryURLRoutes.Account.CheckAuthorize);
            if (!User.Identity.IsAuthenticated || !checkAuthorize.IsSuccess)
                return RedirectToAction("Login", "Account");

            //var adminDashboard = await _httpClient.GetAsync<GetAdminDashboardDTO>(CoreURLRoutes.Dashboard.GetHomeDashboard);
            //return View(adminDashboard.Value);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult StyleEn()
        {
            Response.ContentType = "text/css";
            return View();
        }

        public ActionResult StyleAr()
        {
            Response.ContentType = "text/css";
            return View();
        }

        public ActionResult MainStyle()
        {
            Response.ContentType = "text/css";
            return View();
        }
    }
}