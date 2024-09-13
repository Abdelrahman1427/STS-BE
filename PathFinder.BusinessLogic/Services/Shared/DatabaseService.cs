using AutoMapper;
using STS.Core.Interface.Shared.IServices;
using STS.Core.Interfaces.Shared.Repository;
using STS.DataTransferObjects.Helpers;
using STS.SharedKernel.Constants;

namespace STS.BusinessLogic.Services.Shared
{
    public class DatabaseService : ILoggerTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public DatabaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<APIResult> SaveAction(Logger logger)
        {
            try
            {
                var _entity = _unitOfWork.GetRepositoryAsync<Core.Entities.Logger>();
                var _mapperDto = _mapper.Map<Core.Entities.Logger>(logger);
                await _entity.AddAsync(_mapperDto);
                await _unitOfWork.SaveChangesAsync();
                if (_unitOfWork.Complete() > 0)
                    return new APIResult { state = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return new APIResult { message = ex.InnerException.Message };
                return new APIResult { message = ex.Message };
            }
            return new APIResult { state = false, message = ExceptionConstants.FailedToSaveActionInLogger };
        }
    }
}
