using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;


namespace PathFinder.SharedKernel.Enums
{
    public enum Gender
    {
        [Display(ResourceType = typeof(CoreResources), Name = "Male")]
        Male = 1,
        [Display(ResourceType = typeof(CoreResources), Name = "Female")]
        Female = 2,
        [Display(ResourceType = typeof(CoreResources), Name = "Other")]
        Other = 3,
        [Display(ResourceType = typeof(CoreResources), Name = "NoAnswer")]
        NoAnswer = 4
    }
}
