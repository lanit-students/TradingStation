using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DTO;
using DTO.MarketBrokerObjects;
using System;

namespace DataBaseService.Mappers
{
    public class TradeMapper : ITradeMapper
    {
        public DbTransaction MapToDbTransaction(Transaction transaction)
        {
            var time = new TimeSpan(transaction.DateTime.Hour, transaction.DateTime.Minute, transaction.DateTime.Second);
            return new DbTransaction
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Broker = transaction.Broker.ToString(),
                Figi = transaction.Figi,
                Operation = transaction.Operation.ToString(),
                Count = transaction.Count,
                Price = transaction.Price,
                Currency = transaction.Currency.ToString(),
                Date = transaction.DateTime.Date,
                Time = time,
                IsSuccess = transaction.IsSuccess
            };
        }

        public Transaction MapToTransaction(DbTransaction dbTransaction)
        {
            var dateTime = new DateTime(
                dbTransaction.Date.Year,
                dbTransaction.Date.Month,
                dbTransaction.Date.Day,
                dbTransaction.Time.Hours,
                dbTransaction.Time.Minutes,
                dbTransaction.Time.Seconds
                );
            return new Transaction
            {
                Id = dbTransaction.Id,
                UserId = dbTransaction.UserId,
                Broker = (BrokerType)Enum.Parse(typeof(BrokerType), dbTransaction.Broker),
                Figi = dbTransaction.Figi,
                Operation = (OperationType)Enum.Parse(typeof(OperationType), dbTransaction.Operation),
                Count = dbTransaction.Count,
                Price = dbTransaction.Price,
                Currency = (Currency)Enum.Parse(typeof(Currency), dbTransaction.Currency),
                IsSuccess = dbTransaction.IsSuccess,
                DateTime = dateTime
            };
        }

        public UserBalance MapToUserBalance(DbUserBalance dbUserBalance)
        {
            return new UserBalance()
            {
                UserId = dbUserBalance.UserId,
                BalanceInRub = dbUserBalance.BalanceInRub,
                BalanceInUsd = dbUserBalance.BalanceInUsd,
                BalanceInEur = dbUserBalance.BalanceInEur
            };
        }

        public DbUserBalance MapToDbUserBalance(UserBalance userBalance)
        {
            return new DbUserBalance()
            {
                UserId = userBalance.UserId,
                BalanceInRub = userBalance.BalanceInRub,
                BalanceInUsd = userBalance.BalanceInUsd,
                BalanceInEur = userBalance.BalanceInEur
            };
        }

        public InstrumentData MapToInstrument(DbPortfolio dbPortfolio)
        {
            return new InstrumentData()
            {
                Figi = dbPortfolio.Figi,
                Count = dbPortfolio.Count,
                Broker = Enum.Parse<BrokerType>(dbPortfolio.Broker)
            };
        }
    }
}
