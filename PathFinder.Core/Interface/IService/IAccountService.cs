using PathFinder.Core.Entities;
using PathFinder.Core.Interface.Shared.IServices;
using PathFinder.DataTransferObjects.DTOs.Account;
using PathFinder.DataTransferObjects.DTOs.Identity.Request;
using PathFinder.DataTransferObjects.Helpers;
namespace PathFinder.Core.Interface.IService
{
    public interface IAccountService: ICrudWithPaginateService<User>
    {
       // Task<APIResult> Register(RegisterDTO registerDTO);
        Task<APIResult> DeleteAsync(int Id);
        Task<APIResult> Login(LoginRequestDTO loginRequestDTO);
        Task<APIResult> GetUser();
       // Task<APIResult> UpdateStatus(UserStatusDTO userStatusDTO);
     //   Task<APIResult> UpdateUser(ViewUpdateProfileDTO viewUpdateProfileDTO);
        Task<APIResult> ForgetPassword(SendForgotPasswordDTO sendForgotPasswordDTO);
        Task<APIResult> ResetPassword(ResetPasswordDTO resetPasswordDto);
       // Task<APIResult> ChangePassword(ChangePasswordDTO changePasswordDto);
        Task<APIResult> CheckCode(ForgotPasswordDTO resetPasswordDto);
        Task<APIResult> RefreshTokenAsync(string token);
        Task<APIResult> GetById(int id);
        Task<APIResult> Logout(string token);
    }
}
