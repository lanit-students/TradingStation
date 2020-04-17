using System.Collections.Generic;
using Tinkoff.Trading.OpenApi.Models;

namespace Interfaces
{
    public interface IBroker
    {
        IEnumerable<IMarketInstrument> GetInstruments(InstrumentType type);

        /// <summary>
        /// Depth of market glass
        /// </summary>
        int Depth { get; set; }
    }
}
