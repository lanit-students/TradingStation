using DataBaseService.Database.Models;
using DTO;
using DTO.MarketBrokerObjects;

namespace DataBaseService.Mappers.Interfaces
{
    public interface ITradeMapper
    {
        DbTransaction MapToDbTransaction(Transaction transaction);
        Transaction MapToTransaction(DbTransaction Dbtransaction);
        DbBrokerUser MapToDbBrokerUser(BrokerUser brokerUser);
        BrokerUser MapToBrokerUser(DbBrokerUser dbBrokerUser);
    }
}
