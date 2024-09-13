using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Clients;

namespace PathFinder.WebApp.Controllers;

public class TermsAndConditionsController : Controller
{
    private readonly IHttpRequestClient _httpClient;
    public TermsAndConditionsController(IHttpRequestClient httpClient)
    {
        _httpClient = httpClient;
    }

    //[AllowAnonymous]
    //public async Task<IActionResult> ViewTermsAndConditionsForLogin()
    //{
    //    var model = await _httpClient.GetAsync<CompanyTermsAndConditionsDTO>($"{CoreURLRoutes.Settings.GetTermsAndConditionsForLogin}");

    //    return View(model.Value);
    //}
}

