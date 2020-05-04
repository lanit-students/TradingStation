using LogReader.Database;
using LogReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogReader.ViewModels
{
    public class LogViewModel
    {
        public readonly LogContext LogContext;

        public LogViewModel(LogContext context)
        {
            LogContext = context;
        }

        public List<Log> GetLogs()
        {
            return LogContext.Logs.ToList();
        }
    }
}
