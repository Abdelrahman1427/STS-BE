using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;


namespace PathFinder.SharedKernel.Enums
{
    public enum Season
    {
        [Display(ResourceType = typeof(CoreResources), Name = "Winter")]
        Winter =1,
        [Display(ResourceType = typeof(CoreResources), Name = "Summer")]
        Summer =2,
        [Display(ResourceType = typeof(CoreResources), Name = "Spring")]
        Spring =3,
        [Display(ResourceType = typeof(CoreResources), Name = "Autumn")]
        Autumn = 4
    }
}
