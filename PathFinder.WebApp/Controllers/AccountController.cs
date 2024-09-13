using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using PathFinder.WebApp.Clients;
using PathFinder.DataTransferObjects.DTOs.Account;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.SharedKernel.Enums;
using Newtonsoft.Json;
using AutoMapper;
using PathFinder.WebApp.Models;
using PathFinder.SharedKernel.Extensions;
using PathFinder.Common.Helpers.Enums;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.DTOs.Employee;
using PathFinder.DataTransferObjects.DTOs.Identity.Response;
using PathFinder.DataTransferObjects.DTOs.Identity.Request;
using PathFinder.WebApp.Areas.DataEntry;
using PathFinder.WebApp.Areas;

namespace PathFinder.WebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : CrudController<AddEmployeeDTO, UpdateEmployeeDTO>
    {
        private IHttpRequestClient _httpClient;
        private readonly IHttpContextAccessor _context;
        private readonly IMapper _mapper;
        public AccountController(IHttpRequestClient httpClient, IHttpContextAccessor context, IMapper mapper)
        : base(httpClient, CoreURLRoutes.Employee.EmployeeDefault, "_addEmployee", "_updateEmployee")
        {
            _httpClient = httpClient;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) });

            return Redirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [CustomAuthorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [CustomAuthorize]
        public virtual async Task<JsonResult> LoadDataTable(DataTableModel data)
        {
            var page = new PagingDTO()
            {
                PageIndex = data.PageNo,
                PageSize = data.Length,
                SortingDTO = new SortingDTO
                {
                    IsOrderAsc = data.Order[0].Dir == "asc" ? true : false,
                    OrderBy = data.Columns[data.Order[0].Column].Name,
                }
            };
            var result = await _httpClient.PostAsync<Pagination<GetPageEmployeeDTO>, PagingDTO>(CoreURLRoutes.Employee.EmployeePage, page);
            Pagination<GetPageEmployeeDTO> getPageEmployeeDTO = JsonConvert.DeserializeObject<Pagination<GetPageEmployeeDTO>>(result.Value.entity?.ToString())!;
            return Json(data.GetDataTableModel(getPageEmployeeDTO));
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? message)
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            var checkAuthorize = await _httpClient.GetAsync<APIResult>(DataEntryURLRoutes.Account.CheckAuthorize);
            if (User.Identity.IsAuthenticated && checkAuthorize.IsSuccess)
                return RedirectToAction("Index", "Home");

            if (message != null)
            {
                if (message == ModelValidationResources.SuccessMessage)
                    ViewBag.SuccessMessage = message;
                else
                    ViewBag.ErrorMessage = message;
            }
            return View(loginRequestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var result = await _httpClient.PostAsync<LoginResponseDTO, LoginRequestDTO>(DataEntryURLRoutes.Account.Login, login);
            if (!result.IsSuccess)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(login);
            }
            else if (!result.Value.state)
            {
                ViewBag.ErrorMessage = result.Value.message;
                return View(login);
            }
            LoginResponseDTO loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(result.Value.entity?.ToString())!;
            var claims = loginResponseDTO?.Claims.Select(a => new Claim(a.Key, a.Value)).ToList();

            await HttpContext.SignInAsync(
               new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
               new AuthenticationProperties
               {
                   IsPersistent = login.RememberLogin,
                   AllowRefresh = false,
                   ExpiresUtc = loginResponseDTO?.RefreshTokenExpiration
               });

            HttpContext.Session.SetString("Token", loginResponseDTO?.Token);
            HttpContext.Response.Cookies.Append("Token", loginResponseDTO?.Token, new CookieOptions() { Expires = loginResponseDTO?.RefreshTokenExpiration.UtcDateTime.ToLocalTime() });

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [Authorize]
        [CustomAuthorize]
        public async Task<ActionResult> Logout()
        {
            string token = HttpContext.Session.GetString("Token");
            await _httpClient.GetAsync<APIResult>($"{DataEntryURLRoutes.Account.Logout}{token}");
            HttpContext.Session.Remove("Token");
            HttpContext.Response.Cookies.Delete("Token");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new SendForgotPasswordDTO());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(SendForgotPasswordDTO passwordDTO)
        {
            var result = await _httpClient.PostAsync<ForgotPasswordDTO, SendForgotPasswordDTO>(DataEntryURLRoutes.Account.ForgetPassword, passwordDTO);
            if (result.IsSuccess)
            {
                ForgotPasswordDTO forgotPasswordDTO = JsonConvert.DeserializeObject<ForgotPasswordDTO>(result.Value.entity?.ToString())!;
                forgotPasswordDTO.Code = string.Empty;
                return View("CheckCode", forgotPasswordDTO);
            }
            else if (!result.IsSuccess)
                ViewBag.ErrorMessage = result.Message;
            else if (!result.Value.state)
                ViewBag.ErrorMessage = result.Value.message;

            return View("ForgotPassword", passwordDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CheckCode(ForgotPasswordDTO forgotPasswordDTO)
        {
            var result = await _httpClient.PostAsync<CheckCodeDTO, ForgotPasswordDTO>(DataEntryURLRoutes.Account.CheckCode, forgotPasswordDTO);
            if (result.IsSuccess)
            {
                CheckCodeDTO checkCodeDTO = JsonConvert.DeserializeObject<CheckCodeDTO>(result.Value.entity?.ToString())!;
                if (checkCodeDTO.isSuccessCode)
                {
                    ResetPasswordDTO resetPassword = new ResetPasswordDTO()
                    {
                        Email = checkCodeDTO.Email,
                        Code = checkCodeDTO.Code,
                        Token = checkCodeDTO.Token
                    };
                    return View("ResetPassword", resetPassword);
                }
                ViewBag.ErrorMessage = CoreResources.CodeWrong;
            }
            else if (!result.IsSuccess)
                ViewBag.ErrorMessage = result.Message;
            else if (!result.Value.state)
                ViewBag.ErrorMessage = result.Value.message;

            return View("CheckCode", forgotPasswordDTO);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordDTO);

            var response = await _httpClient.PostAsync<APIResult, ResetPasswordDTO>(DataEntryURLRoutes.Account.ResetPassword, resetPasswordDTO);
            if (response.Value.state)
                return RedirectToAction("Login", "Account", new { area = "User", message = ModelValidationResources.SuccessMessage });

            ViewBag.ErrorMessage = ModelValidationResources.PasswordValidation;
            return View("ResetPassword", resetPasswordDTO);
        }

        [HttpGet]
        [Authorize]
        [CustomAuthorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordDTO());
        }

        [HttpPost]
        [Authorize]
        [CustomAuthorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            var result = await _httpClient.PostAsync<APIResult, ChangePasswordDTO>(DataEntryURLRoutes.Account.ChangePassword, passwordDTO);
            if (result.IsSuccess)
                return await Logout();
            else if (!result.IsSuccess)
                ViewBag.ErrorMessage = result.Message;
            else if (!result.Value.state)
                ViewBag.ErrorMessage = result.Value.message;

            return View("ChangePassword", passwordDTO);
        }

		[HttpGet]
		public IActionResult UserType()
		{
			return View();
		}
	}
}
