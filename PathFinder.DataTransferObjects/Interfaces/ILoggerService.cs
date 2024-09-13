
using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.SharedKernel.Interfaces
{
    public interface ILoggerService
    {
        public Task<APIResult> Log(string LogType, string Path, string Message);
    }
}
