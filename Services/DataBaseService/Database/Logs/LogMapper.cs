using DataBaseService.Database.Logs.Interfaces;
using DataBaseService.Database.Models;
using Kernel;

namespace DataBaseService.Database.Logs
{
    public class LogMapper : ILogMapper
    {
        public DbLog Map(LogMessage log)
        {
            return new DbLog
            {
                Id = log.Id,
                Message = log.Message,
                ParentId = log.ParentId ?? null,
                Level = (int)log.Level,
                Time = log.Time,
                ServiceName = log.ServiceName
            };
        }
    }
}
