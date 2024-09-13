using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Notification
{
    public class NotificationsDTO : GetLookUpDefinitionDTO
    {
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string SenderName { get; set; }
        public string ReciverName { get; set; } = "Admin";
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public string Message { get; set; }
        public string MessageType { get; set; } = "Personal";
        public DateTime NotificationDateTime { get; set; } = DateTime.Now;
    }
}
