using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.SharedKernel.Enums
{
    public enum Language
    {
        [Display(ResourceType = typeof(CoreResources), Name = "en-US")] Eng = 1,
        [Display(ResourceType = typeof(CoreResources), Name = "ar-EG")] Ar
    }
}
