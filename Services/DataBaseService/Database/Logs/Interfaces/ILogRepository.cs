using Kernel;

namespace DataBaseService.Database.Logs.Interfaces
{
    interface ILogRepository
    {
        void AddLogs(Log log);
    }
}
