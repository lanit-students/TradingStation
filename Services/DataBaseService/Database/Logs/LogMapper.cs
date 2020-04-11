using DataBaseService.Database.Logs.Interfaces;
using DataBaseService.Database.Models;
using Kernel;

namespace DataBaseService.Database.Logs
{
    public class LogMapper : ILogMapper
    {
        public Log MapLog(DbLog dbLog)
        {
            return new Log
            {
                Id = dbLog.Id,
                Message = dbLog.Message,
                ParentId = dbLog.ParentId,
                Type = dbLog.Type,
                Time = dbLog.Time,
                ServiceName = dbLog.ServiceName
            };
        }

        public DbLog MapToDbLog(Log log)
        {
            return new DbLog
            {
                Id = log.Id,
                Message = log.Message,
                ParentId = log.ParentId,
                Type = log.Type,
                Time = log.Time,
                ServiceName = log.ServiceName
            };
        }
    }
}
