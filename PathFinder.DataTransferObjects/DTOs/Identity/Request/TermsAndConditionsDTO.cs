using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class TermsAndConditionsDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string EnName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string ArName { get; set; }
    }
}
