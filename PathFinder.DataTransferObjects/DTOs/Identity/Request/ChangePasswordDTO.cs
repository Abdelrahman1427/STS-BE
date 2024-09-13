
using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%#*?&_])[A-Za-z\d@$!#%*_?&]{8,}$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "PasswordValidation")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}