using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;


namespace PathFinder.SharedKernel.Enums
{
    public enum OwnershipType
    {
        [Display(ResourceType = typeof(CoreResources), Name = "Owner")]
        Owner = 1,
        [Display(ResourceType = typeof(CoreResources), Name = "Tenant")]
        Tenant = 2
    }
}
