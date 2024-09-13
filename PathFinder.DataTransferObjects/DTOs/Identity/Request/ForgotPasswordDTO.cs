
using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class ForgotPasswordDTO
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-zA-Z]{2,3})$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "EmailValidation")]
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Code { get; set; }
    }
    public class SendForgotPasswordDTO
    {
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "EmailEmpty")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-zA-Z]{2,3})$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "EmailValidation")]
        public string Email { get; set; }
    }
}