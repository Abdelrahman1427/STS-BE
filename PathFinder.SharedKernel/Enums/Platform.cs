using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.SharedKernel.Enums
{
    public enum Platform
    {
        [Display(ResourceType = typeof(CoreResources), Name = "IOS")] IOS = 1,
        [Display(ResourceType = typeof(CoreResources), Name = "Android")] Android,
        [Display(ResourceType = typeof(CoreResources), Name = "Website")] Website
    }
}
