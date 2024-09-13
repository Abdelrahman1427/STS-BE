using PathFinder.DataTransferObjects.Helpers;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.Validators;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Account
{
    public class AdminDTO
    {
        public string? Id { get; set; } = null;
        public int? RoleId { get; set; }

        #region Name
        [RegularExpression("^(?=.{2,30}$)[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+(?:\\s[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+)?$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CharacterOnly")]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "FirstNameEmpty")]
        public string FirstName { get; set; }

        [RegularExpression("^(?=.{2,32}$)[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+(?:\\s[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+)?$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CharacterOnly")]
        public string? MiddleName { get; set; }

        [RegularExpression("^(?=.{2,32}$)[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+(?:\\s[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+)?$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CharacterOnly")]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "LastNameEmpty")]
        public string LastName { get; set; }

        #endregion

        #region registration email phone password
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-zA-Z]{2,3})$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ValidEmail")]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "EmailEmpty")]
        public string Email { get; set; }

        [RegularExpression(@"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\d{9,14}$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ValidPhone")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "PasswordEmpty")]
        [PasswordValidation(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ConfirmPasswordEmpty")]
        [Compare("Password", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ConfirmPasswordValidation")]
        public string ConfirmPassword { get; set; }

        #endregion

        #region nationality address center zipcode
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "NationalityEmpty")]
        public int? NationalityId { get; set; }

        public string? Address { get; set; }
        [Range(0, double.MaxValue, ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ValidDoubleNumber")]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string Latitude { get; set; }
        [Range(0, double.MaxValue, ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ValidDoubleNumber")]
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string Longitude { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CenterEmpty")]
        public int CenterId { get; set; }

        [RegularExpression(@"^\d{5}$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "PleaseEnterAValidNumber")]
        public long? ZipCode { get; set; }

        #endregion
        [RequiredCheckbox(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CheckTermsConditionsEmpty")]
        public bool IsTermsConditions { get; set; } = false;

    }
}
