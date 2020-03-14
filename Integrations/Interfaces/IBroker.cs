using DTO;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IBroker
    {
        List<ExchangeRate> GetExchangeRates();
    }
}
