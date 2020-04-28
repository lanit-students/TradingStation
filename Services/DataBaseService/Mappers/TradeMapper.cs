using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DTO;
using DTO.MarketBrokerObjects;
using Kernel.CustomExceptions;
using System;

namespace DataBaseService.Mappers
{
    public class TradeMapper : ITradeMapper
    {
        public DbTransaction MapToDbTransaction(Transaction transaction)
        {
            var date = new DateTime(
                transaction.DateTime.Year,
                transaction.DateTime.Month,
                transaction.DateTime.Minute
                );
            return new DbTransaction
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Broker = transaction.Broker.ToString(),
                Figi = transaction.Figi,
                Operation = transaction.Operation.ToString(),
                Lots = transaction.Lots,
                Price = transaction.Price,
                Date = date,
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
                Broker = dbTransaction.Broker == "TinkoffBroker" ? BrokerType.TinkoffBroker : throw new BadRequestException(),
                Figi = dbTransaction.Figi,
                Operation = dbTransaction.Operation == "Sell" ? OperationType.Sell : OperationType.Buy,
                Lots = dbTransaction.Lots,
                Price = dbTransaction.Price,
                IsSuccess = dbTransaction.IsSuccess,
                DateTime = dateTime
            };
        }

        public BrokerUser MapToBrokerUser(DbBrokerUser dbBrokerUser)
        {
            return new BrokerUser()
            {
                Id = dbBrokerUser.Id,
                UserId = dbBrokerUser.UserId,
                Broker = (BrokerType)Enum.Parse(typeof(BrokerType), dbBrokerUser.Broker),
                BalanceInRub = dbBrokerUser.BalanceInRub,
                BalanceInUsd = dbBrokerUser.BalanceInUsd,
                BalanceInEur = dbBrokerUser.BalanceInEur
            };
        }

        public DbBrokerUser MapToDbBrokerUser(BrokerUser brokerUser)
        {
            return new DbBrokerUser()
            {
                Id = brokerUser.Id,
                UserId = brokerUser.UserId,
                Broker = brokerUser.Broker.ToString(),
                BalanceInRub = brokerUser.BalanceInRub,
                BalanceInUsd = brokerUser.BalanceInUsd,
                BalanceInEur = brokerUser.BalanceInEur
            };
        }
    }
}
