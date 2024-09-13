using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;


namespace PathFinder.SharedKernel.Enums
{
    public enum Port
    {
        [Display(ResourceType = typeof(CoreResources), Name = "Client")]
        Client = 1,
        [Display(ResourceType = typeof(CoreResources), Name = "User")]
        User = 2
    }
}
