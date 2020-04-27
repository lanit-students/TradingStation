using DataBaseService.Database.Models;
using DTO;

namespace DataBaseService.Mappers.Interfaces
{
    public interface ITradeMapper
    {
        DbTransaction MapToDbTransaction(Transaction transaction);
        Transaction MapToTransaction(DbTransaction Dbtransaction);
    }
}
