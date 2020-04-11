using Kernel;

namespace DataBaseService.Database.Logs.Interfaces
{
    public interface ILogRepository
    {
        void Save(LogMessage log);
    }
}
