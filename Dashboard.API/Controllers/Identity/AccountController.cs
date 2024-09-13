using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PathFinder.SharedKernel.Filters;
using PathFinder.Core.Interface.IService;
using PathFinder.DataTransferObjects.DTOs.Account;
using PathFinder.DataTransferObjects.DTOs.Identity.Request;
using PathFinder.DataTransferObjects.Helpers;

namespace STS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        private void SetRefreshTokenInCookie(string refreshToken,DateTimeOffset expire)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = expire.UtcDateTime.ToLocalTime()
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOption);
        }

        [HttpGet("CheckAuthorize")]
        [Authorize]
        [LoggerAction]
        public virtual async Task<APIResult> CheckAuthorize()
        {
            try
            {
                return new APIResult { state = true };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        [HttpPost("Login")]
        [LoggerAction]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _accountService.Login(loginRequestDTO));
        }

        [HttpGet("GetUser")]
        [Authorize]
        [LoggerAction]
        public async Task<IActionResult> GetUser()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _accountService.GetUser());
        }

        [HttpPost("ForgetPassword")]
        [LoggerAction]
        public async Task<IActionResult> ForgetPassword(SendForgotPasswordDTO sendForgotPasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _accountService.ForgetPassword(sendForgotPasswordDTO));
        }

        [HttpPost("ResetPassword")]
        [LoggerAction]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDto)
        {
            var result = await _accountService.ResetPassword(resetPasswordDto);
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        [LoggerAction]
        public async Task<IActionResult> RefreshToken(UpdateRefreshTokenDTO updateRefreshTokenDTO)
        {
            var refreshToken = updateRefreshTokenDTO.Token ?? Request.Cookies["refreshToken"];
            var result = await _accountService.RefreshTokenAsync(refreshToken);
            SetRefreshTokenInCookie(result.entity?.RefreshTokenKey, result.entity?.RefreshTokenExpiration);
            return Ok(result);
        }

        [HttpPost("CheckCode")]
        [LoggerAction]
        public async Task<IActionResult> CheckCode(ForgotPasswordDTO checkCodeDTO)
        {
            var result = await _accountService.CheckCode(checkCodeDTO);
            return Ok(result);
        }

        [HttpGet("Logout/{token}")]
        [Authorize]
        [LoggerAction]
        public async Task<IActionResult> Logout(string token)
        {
            var result = await _accountService.Logout(token);
            return Ok(result);
        }
    }
}
