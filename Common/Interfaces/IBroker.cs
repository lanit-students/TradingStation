using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IBroker
    {
        IEnumerable<Instrument> GetInstruments(InstrumentType type);
        bool Trade(InternalTradeRequest request);
        int Depth { get; set; }
    }
}
