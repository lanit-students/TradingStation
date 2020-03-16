using DTO;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IBroker
    {
        List<IMarketInstrument> GetCurrencies();
        IMarketInstrument GetCurrency(string idCurrency);
        List<IMarketInstrument> GetStocks();
        IMarketInstrument GetStock(string idStock);
        List<IMarketInstrument> GetBonds();
        IMarketInstrument GetBond(string idBond);
    }
}
