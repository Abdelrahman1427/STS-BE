using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.Core.Interface.Shared.IServices
{
    public interface ILoggerTypeService
    {
        public Task<APIResult> SaveAction(Logger logger);
    }
}
