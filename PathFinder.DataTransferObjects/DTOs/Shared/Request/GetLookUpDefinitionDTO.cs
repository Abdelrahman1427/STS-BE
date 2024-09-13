using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Shared.Request
{
    public class GetLookUpDefinitionDTO : GetPageDefinitionDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "EngNameEmpty")]
        public string EnName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "ArNameEmpty")]
        public string ArName { get; set; }
    }
}
