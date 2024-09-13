
using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Role
{
    public class RoleForCreationDTO
    {
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
