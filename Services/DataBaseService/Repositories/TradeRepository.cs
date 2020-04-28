using DataBaseService.Database;
using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DataBaseService.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly ITradeMapper mapper;
        private readonly TPlatformDbContext dbContext;
        private readonly ILogger<TradeRepository> logger;

        public TradeRepository(
            ITradeMapper mapper, TPlatformDbContext dbContext, 
            [FromServices] ILogger<TradeRepository> logger
            )
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

        public Instrument GetInstrumentFromPortfolio(GetInstrumentFromPortfolioRequest request)
        {
            var instrument = dbContext.PortfolioInstruments.FirstOrDefault(
                instrument => instrument.UserId == request.UserId && instrument.Figi == request.Figi);
            if (instrument == null)
                return new Instrument();
            return new Instrument()
            {
                Figi = request.Figi,
                Lot = instrument.Lots,
                Type = (InstrumentType)Enum.Parse(typeof(InstrumentType), instrument.InstrumentType)
            };
        }

        public BrokerUser GetBrokerUser(GetBrokerUserRequest request)
        {
            
            
            return new BrokerUser()
            {

            };
        }
        
        private void RegisterTinkoffUser(Guid userId)
        {
            var tinkoffUser = new DbBrokerUser()
            {
                UserId = userId,
                BalanceInRub = 0,
                BalanceInUsd = 0,
                BalanceInEur = 0,
                Broker = BrokerType.TinkoffBroker.ToString(),
                Id = Guid.NewGuid()
            };
            dbContext.BrokerUsers.Add(tinkoffUser);
            dbContext.SaveChanges();
        }


    }
}
