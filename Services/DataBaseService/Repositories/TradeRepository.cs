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
            var dbBrokerUser = dbContext.BrokerUsers.FirstOrDefault(
                user => user.Broker == request.Broker.ToString() && user.UserId == request.UserId);
            if (dbBrokerUser == null)
            {
                var newBrokerUser= new BrokerUser()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Broker = request.Broker,
                    BalanceInRub = 0,
                    BalanceInUsd = 0,
                    BalanceInEur = 0
                };
                RegisterBrokerUser(newBrokerUser);
                return newBrokerUser;
            }
            return mapper.MapToBrokerUser(dbBrokerUser);
        }

        public void UpdateBrokerUser(BrokerUser brokerUser)
        {
            var dbBrokerUser = dbContext.BrokerUsers.FirstOrDefault(
                user => user.Broker == brokerUser.Broker.ToString() && user.UserId == brokerUser.UserId);
            dbBrokerUser.BalanceInRub = brokerUser.BalanceInRub;
            dbBrokerUser.BalanceInUsd = brokerUser.BalanceInUsd;
            dbBrokerUser.BalanceInEur = brokerUser.BalanceInEur;
            dbContext.SaveChanges();
        }
        
        private void RegisterBrokerUser(BrokerUser brokerUser)
        {
            var dbBrokerUser = mapper.MapToDbBrokerUser(brokerUser);
            dbContext.BrokerUsers.Add(dbBrokerUser);
            dbContext.SaveChanges();
        }
    }
}
