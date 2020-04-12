using DataBaseService.Database;
using DataBaseService.Database.Logs.Interfaces;
using Kernel;

namespace DataBaseService.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ILogMapper mapper;
        private readonly TPlatformLogDbContext dbContext;

        public LogRepository(ILogMapper mapper, TPlatformLogDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public void Save(LogMessage log)
        {
            dbContext.Logs.Add(mapper.Map(log));
            dbContext.SaveChanges();
        }
    }
}
