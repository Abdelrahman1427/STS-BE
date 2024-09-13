namespace STS.Core.Interface.Shared.IServices
{
    public interface ILoggerFactoryService
    {
        public ILoggerTypeService GetLoggerType(string type);
    }
}
