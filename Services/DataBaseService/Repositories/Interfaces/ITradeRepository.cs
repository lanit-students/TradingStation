using DTO;

namespace DataBaseService.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        void SaveTransaction(Transaction transaction);
    }
}
