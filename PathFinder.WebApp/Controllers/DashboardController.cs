using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PathFinder.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
