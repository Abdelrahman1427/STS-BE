using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Account
{
    public class ViewUpdateProfileDTO
    {
        public string? Id { get; set; } = null;
        [RegularExpression("^(?=.{2,32}$)[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+(?:\\s[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+)?$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CharacterOnly")]
        public string? FirstName { get; set; }
        [RegularExpression("^(?=.{2,32}$)[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+(?:\\s[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+)?$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CharacterOnly")]
        public string? MiddleName { get; set; }
        [RegularExpression("^(?=.{2,32}$)[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+(?:\\s[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+)?$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CharacterOnly")]
        public string? LastName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-zA-Z]{2,3})$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ValidEmail")]
        public string? Email { get; set; }
        [RegularExpression(@"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\d{9,14}$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ValidPhone")]
        public string? PhoneNumber { get; set; }

        [RegularExpression("^(?=.{2,30}$)[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+(?:\\s[\\u0600-\\u065F\\u066A-\\u06EF\\u06FA-\\u06FFa-zA-Z]+)?$", ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "CharacterOnly")]
        public string? RegisterName { get; set; }
    }
}
