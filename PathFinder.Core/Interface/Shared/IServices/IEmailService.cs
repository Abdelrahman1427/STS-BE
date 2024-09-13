using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace PathFinder.Core.Interface.Shared.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO emailRequest);
    }
}
