
using Microsoft.AspNetCore.Mvc;

namespace PathFinder.WebApp.Controllers;

public class ErrorsController : Controller
{
    public IActionResult Error()
    {
        return View();
    }
}
