using STS.Core.Interface.Shared.IServices;
using STS.Core.Interfaces.Shared.Repository;
using STS.DataTransferObjects.Helpers;
using STS.SharedKernel.Interfaces;
using System.Text.RegularExpressions;

namespace STS.BusinessLogic.Services.Shared
{
    public class LoggerService: ILoggerService
    {
        private readonly ILoggerFactoryService _loggerFactoryService;
        private Logger logger;

        public LoggerService(ILoggerFactoryService loggerFactoryService)
        {
            _loggerFactoryService = loggerFactoryService;
            logger = new Logger();
        }
        public async Task<APIResult> Log(string LogType, string Path, string Message)
        {
            ILoggerTypeService _logTypeService = _loggerFactoryService.GetLoggerType(LogType);
            logger.Path = Path;
            logger.Message = Message;
            logger.Path = Regex.Replace(Path, "[0-9]", "");
            logger.CreatedBy = "Abdelrahman";
            logger.CreatedDate = DateTime.Now;

            var apiResult =  await _logTypeService.SaveAction(logger);
            return apiResult;
        }
    }
}
