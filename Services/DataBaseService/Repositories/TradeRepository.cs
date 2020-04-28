using DataBaseService.Database;
using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Kernel.CustomExceptions;
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

        private void UpdateBalanceAfterTransaction(Transaction transaction)
        {
            var dbUserBalance = dbContext.UserBalances.FirstOrDefault(
                user => user.UserId == transaction.UserId);

            var sign = transaction.Operation == OperationType.Sell ? 1 : -1;
            var cost = sign * transaction.Count * transaction.Price;

            switch (transaction.Currency)
            {
                case Currency.RUB:
                    dbUserBalance.BalanceInRub += cost;
                    break;
                case Currency.USD:
                    dbUserBalance.BalanceInUsd += cost;
                    break;
                case Currency.EUR:
                    dbUserBalance.BalanceInEur += cost;
                    break;
                default:
                    throw new BadRequestException();
            }
            dbContext.SaveChanges();
        }

        private void UpdatePortfolioAfterTransaction(Transaction transaction)
        {
            var portfolio = dbContext.Portfolios.FirstOrDefault(
                p => p.Figi == transaction.Figi && p.UserId == transaction.UserId);
            
            if(portfolio == null && transaction.Operation == OperationType.Buy)
            {
                portfolio = new DbPortfolio()
                {
                    Figi = transaction.Figi,
                    Count = transaction.Count,
                    UserId = transaction.UserId
                };
                dbContext.Portfolios.Add(portfolio);
            }
            else if (transaction.Operation == OperationType.Sell && portfolio.Count <= 0)
            {
                throw new BadRequestException();
            }
            else
            {
                var sign = transaction.Operation == OperationType.Buy ? 1 : -1;
                var orderLots = sign * transaction.Count;
            } 
        }

        public void SaveTransaction(Transaction transaction)
        {
            dbContext.Transactions.Add(mapper.MapToDbTransaction(transaction));
            
            dbContext.SaveChanges();
        }

        public Instrument GetInstrumentFromPortfolio(GetInstrumentFromPortfolioRequest request)
        {
            var instrument = dbContext.Portfolios.FirstOrDefault(
                instrument => instrument.UserId == request.UserId && instrument.Figi == request.Figi);
            
            if (instrument == null)
                return new Instrument();
            
            return new Instrument()
            {
                Figi = request.Figi,
                Lot = instrument.Count,
            };
        }

        public UserBalance GetUserBalance(GetUserBalanceRequest request)
        {
            var dbUserBalance = dbContext.UserBalances.FirstOrDefault(
                user => user.UserId == request.UserId);
            
            if (dbUserBalance == null)
            {
                var newUserBalance= new UserBalance()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    BalanceInRub = 0,
                    BalanceInUsd = 0,
                    BalanceInEur = 0
                };
                RegisterBrokerUser(newUserBalance);
                return newUserBalance;
            }
            return mapper.MapToUserBalance(dbUserBalance);
        }

        public void UpdateUserBalance(UserBalance userBalance)
        {
            var dbUserBalance = dbContext.UserBalances.FirstOrDefault(
                user => user.UserId == userBalance.UserId);

            dbUserBalance.BalanceInRub = userBalance.BalanceInRub;
            dbUserBalance.BalanceInUsd = userBalance.BalanceInUsd;
            dbUserBalance.BalanceInEur = userBalance.BalanceInEur;

            dbContext.SaveChanges();
        }
        
        private void RegisterBrokerUser(UserBalance userBalance)
        {
            var dbUserBalance = mapper.MapToDbUserBalance(userBalance);
            dbContext.UserBalances.Add(dbUserBalance);
            dbContext.SaveChanges();
        }
    }
}
