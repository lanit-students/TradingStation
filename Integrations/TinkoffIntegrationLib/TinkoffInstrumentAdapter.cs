using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffIntegrationLib
{
    class TinkoffInstrumentAdapter : IMarketInstrument
    {
        MarketInstrument tinkoffInstrument;
        Orderbook tinkoffOrderbook;

        public TinkoffInstrumentAdapter(MarketInstrument instrument, Orderbook orderbook)
        {
            if (instrument.Figi != orderbook.Figi)
                throw new ArgumentException("Transfer orderbook does not match the market instrument");

            tinkoffInstrument = instrument;
            tinkoffOrderbook = orderbook;
        }

        public string Id => tinkoffInstrument.Figi;

        public string Name => tinkoffInstrument.Name;

        public decimal Price => tinkoffOrderbook.LastPrice;
    }
}
