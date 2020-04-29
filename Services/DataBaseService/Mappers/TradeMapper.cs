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
                Time = transaction.DateTime, 
                IsSuccess = transaction.IsSuccess
            };
        }

        public Transaction MapToTransaction(DbTransaction dbTransaction)
        {
            var dateTime = new DateTime(
                dbTransaction.Date.Year,
                dbTransaction.Date.Month,
                dbTransaction.Date.Day,
                dbTransaction.Time.Hour,
                dbTransaction.Time.Minute,
                dbTransaction.Time.Second
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
                Id = dbUserBalance.Id,
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
                Id = userBalance.Id,
                UserId = userBalance.UserId,
                BalanceInRub = userBalance.BalanceInRub,
                BalanceInUsd = userBalance.BalanceInUsd,
                BalanceInEur = userBalance.BalanceInEur
            };
        }
    }
}
