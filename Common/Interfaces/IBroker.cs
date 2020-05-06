using DTO;
using DTO.BrokerRequests;
using System;
using DTO.MarketBrokerObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBroker
    {
        IEnumerable<Instrument> GetInstruments(InstrumentType type);
        Transaction Trade(InternalTradeRequest request);

        IEnumerable<Candle> SubscribeOnCandle(string Figi, Action<Candle> SendCandle);

        int Depth { get; set; }
    }
}
