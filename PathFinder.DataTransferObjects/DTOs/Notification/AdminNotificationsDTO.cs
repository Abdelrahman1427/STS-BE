using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Notification
{
    public class AdminNotificationsDTO
    {
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string From { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public List<int> To { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string Message { get; set; }
        public string UserId { get; set; }
    }
}
