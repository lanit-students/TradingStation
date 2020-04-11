using DataBaseService.Database.Models;
using Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Database.Logs.Interfaces
{
    interface ILogMapper
    {
        Log MapLog(DbLog dbLog);

        DbLog MapToDbLog(Log log);
    }
}
