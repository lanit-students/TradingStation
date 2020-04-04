using System.Collections.Generic;
using System.Threading.Tasks;

using DTO.NewsRequests.Currency;

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
