using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;


namespace PathFinder.SharedKernel.Enums
{
    public enum Month
	{
		[Display(ResourceType = typeof(CoreResources), Name = "Jan")] Jan = 1,
		[Display(ResourceType = typeof(CoreResources), Name = "Feb")] Feb = 2,
		[Display(ResourceType = typeof(CoreResources), Name = "Mar")] Mar = 3,
		[Display(ResourceType = typeof(CoreResources), Name = "Apr")] Apr = 4,
		[Display(ResourceType = typeof(CoreResources), Name = "May")] May = 5,
		[Display(ResourceType = typeof(CoreResources), Name = "Jun")] Jun = 6,
		[Display(ResourceType = typeof(CoreResources), Name = "Jul")] Jul = 7,
		[Display(ResourceType = typeof(CoreResources), Name = "Aug")] Aug = 8,
		[Display(ResourceType = typeof(CoreResources), Name = "Sep")] Sep = 9,
		[Display(ResourceType = typeof(CoreResources), Name = "Oct")] Oct = 10,
		[Display(ResourceType = typeof(CoreResources), Name = "Nov")] Nov = 11,
		[Display(ResourceType = typeof(CoreResources), Name = "Dec")] Dec = 12,
	}
}
