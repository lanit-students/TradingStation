using DataBaseService.Database;
using DataBaseService.Mappers.Interfaces;
using DataBaseService.Repositories.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataBaseService.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly ITradeMapper mapper;
        private readonly TPlatformDbContext dbContext;
        private readonly ILogger<TradeRepository> logger;

        public TradeRepository(ITradeMapper mapper, TPlatformDbContext dbContext, [FromServices] ILogger<TradeRepository> logger)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void SaveTransaction(Transaction transaction)
        {
            dbContext.Transactions.Add(mapper.MapToDbTransaction(transaction));
            dbContext.SaveChanges();
        }


    }
}
