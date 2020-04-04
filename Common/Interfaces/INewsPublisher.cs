using System.Collections.Generic;

using DTO.CurrencyRequests;

namespace Interfaces
{
    /// <summary>
    /// An interface for each news publisher
    /// </summary>
    public interface INewsPublisher
    {
        List<ExchangeRate> GetCurrencies();
    }
}
