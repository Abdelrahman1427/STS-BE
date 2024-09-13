using AutoMapper;
using STS.Core.Interfaces.Shared.Repository;
using STS.SharedKernel.Constants;
using Microsoft.Extensions.Options;
using RoboGas.DataTransferObjects.Helpers;
using STS.Core.Interface.Shared.IServices;

namespace STS.BusinessLogic.Services.Shared
{
    public class LoggerFactoryService : ILoggerFactoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FileLogs _fileLogs;
        public LoggerFactoryService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<FileLogs> fileLogs)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileLogs = fileLogs.Value;
        }
        public  ILoggerTypeService GetLoggerType(string type)
        {
            if (type == AppConstants.Database)
                return new DatabaseService(_unitOfWork, _mapper);
            if (type == AppConstants.FileExceptions)
                return new FileErrorService(_fileLogs);
            return new FileActionService(_fileLogs);
        }
    }
}
