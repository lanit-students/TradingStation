using DTO;
using DTO.BrokerRequests;

namespace DataBaseService.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        void SaveTransaction(Transaction transaction);
        Instrument GetInstrumentFromPortfolio(GetInstrumentFromPortfolioRequest request);
    }
}
