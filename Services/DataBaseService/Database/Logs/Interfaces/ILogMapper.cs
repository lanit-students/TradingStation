using DataBaseService.Database.Models;
using Kernel;

namespace DataBaseService.Database.Logs.Interfaces
{
    public interface ILogMapper
    {
        DbLog Map(LogMessage dbLog);
    }
}
