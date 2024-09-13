using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.Shared.IServices;
using PathFinder.Core.Interfaces.Shared.Repository;
using PathFinder.DataTransferObjects.DTOs.Notification;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddNotificationsAsync(NotificationsDTO sendNotifications)
        {
            try
            {
                var notification = _mapper.Map<Notification>(sendNotifications);
                var addNotification = _unitOfWork.GetRepositoryAsync<Notification>();
                await addNotification.AddAsync(notification);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
