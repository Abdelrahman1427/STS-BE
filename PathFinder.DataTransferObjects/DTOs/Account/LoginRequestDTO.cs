using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.Validators;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Account
{

    public class LoginRequestDTO
    {
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "UserEmailOrMobileEmpty")]
        public string UserEmailOrMobile { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "PasswordEmpty")]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        [RequiredCheckbox(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CheckTermsConditionsEmpty")]
        public bool TermsAndConditionsAcceptanceFlag { get; set; }

        public string? ReturnUrl { get; set; }
        public string? FCMToken { get; set; }

    }
}