
using STS.DataTransferObjects.Helpers;

namespace STS.SharedKernel.Interfaces
{
    public interface ILoggerService
    {
        public Task<APIResult> Log(string LogType, string Path, string Message);
    }
}
