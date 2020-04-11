using DataBaseService.Database.Models;
using Kernel;

namespace DataBaseService.Database.Logs.Interfaces
{
    public interface ILogMapper
    {
        Log MapLog(DbLog dbLog);

        DbLog MapToDbLog(Log log);
    }
}
