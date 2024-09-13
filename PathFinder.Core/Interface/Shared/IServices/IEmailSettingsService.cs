using PathFinder.Core.Entities;

namespace PathFinder.Core.Interface.Shared.IServices
{
    public interface IEmailSettingsService
    {
        Task<EmailSetting> GetEmailSettingsAsync();
    }
}
