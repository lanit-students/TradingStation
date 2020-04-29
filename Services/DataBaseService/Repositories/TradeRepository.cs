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

        private DbUserBalance RegisterUserBalance(Guid userId)
        {
            var userBalance = new UserBalance()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BalanceInRub = 0,
                BalanceInUsd = 0,
                BalanceInEur = 0
            };
            var dbUserBalance = mapper.MapToDbUserBalance(userBalance);
            dbContext.UserBalances.Add(dbUserBalance);
            dbContext.SaveChanges();
            return dbUserBalance;
        }

        private void UpdateBalanceAfterTransaction(Transaction transaction)
        {
            var dbUserBalance = dbContext.UserBalances.FirstOrDefault(
                user => user.UserId == transaction.UserId);

            var sign = transaction.Operation == OperationType.Sell ? 1 : -1;
            var cost = sign * transaction.Count * transaction.Price;

            switch (transaction.Currency)
            {
                case Currency.Rub:
                    dbUserBalance.BalanceInRub += cost;
                    break;
                case Currency.Usd:
                    dbUserBalance.BalanceInUsd += cost;
                    break;
                case Currency.Eur:
                    dbUserBalance.BalanceInEur += cost;
                    break;
                default:
                    throw new BadRequestException();
            }
            dbContext.SaveChanges();
        }

        private void UpdatePortfolioAfterTransaction(Transaction transaction)
        {
            var dbPortfolio = dbContext.Portfolios.FirstOrDefault(
                p => p.Figi == transaction.Figi && p.UserId == transaction.UserId);
            
            if(dbPortfolio == null && transaction.Operation == OperationType.Buy)
            {
                dbPortfolio = new DbPortfolio()
                {
                    Figi = transaction.Figi,
                    Count = transaction.Count,
                    UserId = transaction.UserId,
                    Broker = transaction.Broker.ToString()
                };
                dbContext.Portfolios.Add(dbPortfolio);
            }
            else if (transaction.Operation == OperationType.Sell && dbPortfolio.Count <= transaction.Count)
            {
                throw new BadRequestException();
            }
            else
            {
                var sign = transaction.Operation == OperationType.Buy ? 1 : -1;
                var orderLots = sign * transaction.Count;
                dbPortfolio.Count += orderLots;
            }
            dbContext.SaveChanges();
        }

        public void SaveTransaction(Transaction transaction)
        {
            try
            {
                dbContext.Transactions.Add(mapper.MapToDbTransaction(transaction));
                UpdateBalanceAfterTransaction(transaction);
                UpdatePortfolioAfterTransaction(transaction);
                dbContext.SaveChanges();
            }
            catch
            {
                throw new BadRequestException();
            }
            
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
                dbUserBalance = RegisterUserBalance(request.UserId);
            }
            return mapper.MapToUserBalance(dbUserBalance);
        }

        public void UpdateUserBalance(UserBalance userBalance)
        {
            var dbUserBalance = dbContext.UserBalances.FirstOrDefault(
                user => user.UserId == userBalance.UserId);
            if (dbUserBalance == null)
                dbUserBalance = RegisterUserBalance(userBalance.Id);
            dbUserBalance.BalanceInRub = userBalance.BalanceInRub;
            dbUserBalance.BalanceInUsd = userBalance.BalanceInUsd;
            dbUserBalance.BalanceInEur = userBalance.BalanceInEur;

            dbContext.SaveChanges();
        }
    }
}
