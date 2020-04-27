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
                Count = transaction.Lots,
                Price = transaction.Price,
                TransactionDate = date,
                TransactionTime = transaction.DateTime, 
                //IsSuccess = transaction.IsSuccess
            };
        }

        public Transaction MapToTransaction(DbTransaction dbTransaction)
        {
            var dateTime = new DateTime(
                dbTransaction.TransactionDate.Year,
                dbTransaction.TransactionDate.Month,
                dbTransaction.TransactionDate.Day,
                dbTransaction.TransactionTime.Hour,
                dbTransaction.TransactionTime.Minute,
                dbTransaction.TransactionTime.Second
                );
            return new Transaction
            {
                Id = dbTransaction.Id,
                UserId = dbTransaction.UserId,
                Broker = dbTransaction.Broker == "TinkoffBroker" ? BrokerType.TinkoffBroker : throw new BadRequestException(),
                Figi = dbTransaction.Figi,
                Operation = dbTransaction.Operation == "Sell" ? OperationType.Sell : OperationType.Buy,
                Lots = dbTransaction.Count,
                Price = dbTransaction.Price,
                //IsSuccess = dbTransaction.IsSuccess,
                DateTime = dateTime
            };
        }
    }
}
