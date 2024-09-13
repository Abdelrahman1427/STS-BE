
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class ResetPasswordDTO
    {
        public string Code { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "PasswordEmpty")]
        [PasswordValidation(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ConfirmPasswordEmpty")]
        [Compare("Password", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ConfirmPasswordValidation")]
        public string ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}