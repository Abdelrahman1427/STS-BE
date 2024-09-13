using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;


namespace PathFinder.SharedKernel.Enums
{
    public enum ProfileStatus
    {
        [Display(ResourceType = typeof(CoreResources), Name = "Pending")]
        Pending = 1,
        [Display(ResourceType = typeof(CoreResources), Name = "Accept")]
        Accept = 2,
        [Display(ResourceType = typeof(CoreResources), Name = "Reject")]
        Reject = 3
    }
}
