using DataBaseService.Database.Models;
using DTO;
using DTO.MarketBrokerObjects;

namespace DataBaseService.Mappers.Interfaces
{
    public interface ITradeMapper
    {
        DbTransaction MapToDbTransaction(Transaction transaction);
        Transaction MapToTransaction(DbTransaction Dbtransaction);
        DbUserBalance MapToDbUserBalance(UserBalance userBalance);
        UserBalance MapToUserBalance(DbUserBalance dbBrokerUser);
        InstrumentData MapToInstrument(DbPortfolio dbPortfolio);
    }
}
