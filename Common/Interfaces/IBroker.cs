using DTO;
using DTO.BrokerRequests;
using System;
using DTO.MarketBrokerObjects;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IBroker
    {
        IEnumerable<Instrument> GetInstruments(InstrumentType type);

        Transaction Trade(InternalTradeRequest request);

        IEnumerable<Candle> SubscribeOnCandle(string Figi, int interval, Action<Candle> SendCandle);
    }
}
