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
using System.Collections.Generic;
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
            var dbUserBalance = new DbUserBalance()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BalanceInRub = 0,
                BalanceInUsd = 0,
                BalanceInEur = 0
            };
            dbContext.UserBalances.Add(dbUserBalance);
            dbContext.SaveChanges();
            logger.LogInformation($"New user {userId} balance added");
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
                    var exception = new BadRequestException("Currency isn't correct");
                    logger.LogWarning(exception, "Request with currency isn't correct");
                    throw exception;
            }
            logger.LogInformation($"Request to update balance of user {transaction.UserId} finished successfully");
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
                logger.LogInformation($"Request to add new instrument{transaction.Figi}" +
                    $"to user {transaction.UserId} portfolio finished successfully");
            }
            else if (transaction.Operation == OperationType.Sell && dbPortfolio.Count < transaction.Count)
            {
                var exception = new BadRequestException("Not enough instrument count to sell");
                logger.LogWarning(exception,
                    $"{Guid.NewGuid()}_User {transaction.UserId} asked to sell more instruments {transaction.Figi} than he has");
                throw exception;
            }
            else
            {
                var sign = transaction.Operation == OperationType.Buy ? 1 : -1;
                var orderLots = sign * transaction.Count;
                dbPortfolio.Count += orderLots;
                logger.LogInformation($"Request to trade instrument{transaction.Figi} of user {transaction.UserId} finished successfully");
            }
        }

        public void SaveTransaction(Transaction transaction)
        {
            try
            {
                dbContext.Transactions.Add(mapper.MapToDbTransaction(transaction));
                UpdateBalanceAfterTransaction(transaction);
                UpdatePortfolioAfterTransaction(transaction);
                dbContext.SaveChanges();
                logger.LogInformation($"Transaction of user {transaction.UserId} finished successfully");
            }
            catch
            {
                var exception = new BadRequestException("Transaction saving failed");
                logger.LogWarning(exception, $"{Guid.NewGuid()}_Couldn't save transaction of user {transaction.UserId}");
                throw exception;
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
                TotalCount = instrument.Count,
            };
        }

        public List<InstrumentData> GetPortfolio(GetPortfolioRequest request)
        {
            var portfolio = dbContext.Portfolios.Where(p => p.UserId == request.UserId);

            if (portfolio == null)
            {
                var exception = new NotFoundException("Portfolio not found");
                logger.LogWarning(exception, $"{Guid.NewGuid()}_Portfolio of user {request.UserId} not found");
                throw exception;
            }

            return portfolio.Select(p => mapper.MapToInstrument(p)).ToList();
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
                dbUserBalance = RegisterUserBalance(userBalance.UserId);

            dbUserBalance.BalanceInRub = userBalance.BalanceInRub;
            dbUserBalance.BalanceInUsd = userBalance.BalanceInUsd;
            dbUserBalance.BalanceInEur = userBalance.BalanceInEur;

            dbContext.SaveChanges();

            logger.LogInformation($"Balance of user {userBalance.UserId} updated");
        }

        public List<Transaction> GetUserTransactions(GetUserTransactions request)
        {
            var dbUserTransactions = dbContext.Transactions
                .Where(transaction => transaction.UserId == request.UserId);

            var userTransactions = new List<Transaction>();

            foreach (DbTransaction transaction in dbUserTransactions)
            {
                userTransactions.Add(mapper.MapToTransaction(transaction));
            }

            return userTransactions;
        }
    }
}
