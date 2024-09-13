using STS.DataTransferObjects.Helpers;

namespace STS.Core.Interface.Shared.IServices
{
    public interface ILoggerTypeService
    {
        public Task<APIResult> SaveAction(Logger logger);
    }
}
