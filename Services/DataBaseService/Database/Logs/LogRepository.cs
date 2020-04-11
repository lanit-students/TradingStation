using DataBaseService.Database.Logs.Interfaces;
using Kernel;
using System;

namespace DataBaseService.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ILogMapper mapper;
        private readonly TPlatformLogDbContext dbContext;
        public void AddLogs(Log log)
        {
            throw new NotImplementedException();
        }
    }
}
