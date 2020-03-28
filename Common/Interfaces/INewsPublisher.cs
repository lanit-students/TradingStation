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
        /// Returned string representation of the latest news from publisher
        /// </summary>
        /// <returns></returns>
        List<ExchangeRate> GetNews();
    }
}
