using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.Notification;

namespace PathFinder.BusinessLogic.Mapping
{
    public class NotificationProfile :  Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationsDTO, Notification>()
                .ReverseMap();
        }
    }
}