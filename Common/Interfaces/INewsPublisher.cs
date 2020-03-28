using DTO;
using System.Collections.Generic;

namespace Interfaces
{
    /// <summary>
    /// An interface for each news publisher
    /// </summary>
    public interface INewsPublisher
    {
        /// <summary>
        /// Returned currencies from publisher
        /// </summary>
        /// <returns>List with currencies</returns>
        List<ExchangeRate> GetCurrencies();
    }
}
