using System.Collections.Generic;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using System.Collections.Generic;

namespace DataBaseService.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        void SaveTransaction(Transaction transaction);
        Instrument GetInstrumentFromPortfolio(GetInstrumentFromPortfolioRequest request);
        UserBalance GetUserBalance(GetUserBalanceRequest request);
        void UpdateUserBalance(UserBalance userBalance);
        List<InstrumentData> GetPortfolio(GetPortfolioRequest request);
        List<Transaction> GetUserTransactions(GetUserTransactions request);
    }
}
