using PathFinder.Core.Entities;
using PathFinder.Core.Interfaces.Shared.Repository;
using PathFinder.SharedKernel.Constants;
using PathFinder.Core.Interface.Shared.IServices;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private IUnitOfWork _unitOfWork;
        public EmailSettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmailSetting> GetEmailSettingsAsync()
        {
            try
            {
            var emaliSettingRepository = _unitOfWork.GetRepositoryAsync<EmailSetting>();
                var companyEmailSettings = await emaliSettingRepository.FirstOrDefaultAsync();

                if (companyEmailSettings == null)
                    throw new KeyNotFoundException(ExceptionConstants.EmailSettingsNotFound);
                return companyEmailSettings;
            }
            catch(Exception ex) 
            {
                throw new KeyNotFoundException(ExceptionConstants.EmailSettingsNotFound);
            }
        }
    }
}
