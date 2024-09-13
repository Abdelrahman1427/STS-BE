using PathFinder.DataTransferObjects.DTOs.Notification;

namespace PathFinder.Core.Interface.Shared.IServices
{
    public interface INotificationService
    {
        Task<bool> AddNotificationsAsync(NotificationsDTO sendNotifications);
    }
}
