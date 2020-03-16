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

        /*
         * If I will pass to ctor tinkoff context,
         * class obtain a lot of excess info
        */
        public TinkoffInstrumentAdapter(MarketInstrument instrument, Orderbook orderbook)
        {
            if (instrument.Figi != orderbook.Figi)
                throw new ArgumentException("Transfer orderbook does not match the market instrument");

            tinkoffInstrument = instrument;
            tinkoffOrderbook = orderbook;
        }

        /// <summary>
        /// Figi e.g.
        /// </summary>
        public string Id => tinkoffInstrument.Figi;

        /// <summary>
        /// Name market instrument
        /// </summary>
        public string Name => tinkoffInstrument.Name;

        /// <summary>
        /// Price by market instrument
        /// </summary>
        public decimal Price => tinkoffOrderbook.LastPrice;
    }
}
