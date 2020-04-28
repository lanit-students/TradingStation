using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;

namespace DataBaseService.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        void SaveTransaction(Transaction transaction);
        Instrument GetInstrumentFromPortfolio(GetInstrumentFromPortfolioRequest request);
        BrokerUser GetBrokerUser(GetBrokerUserRequest request);
    }
}
