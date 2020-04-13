using Interfaces;
using System;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffIntegrationLib
{
    public class TinkoffInstrumentAdapter : IMarketInstrument
    {
        private readonly MarketInstrument tinkoffInstrument;
        private readonly Orderbook tinkoffOrderbook;
        private readonly InstrumentType instrumentType;

        /*
         * If I will pass to ctor tinkoff context,
         * class obtain a lot of excess info
        */
        public TinkoffInstrumentAdapter(InstrumentType type, MarketInstrument instrument, Orderbook orderbook)
        {
            if (instrument.Figi != orderbook.Figi)
                throw new ArgumentException("Transfer orderbook does not match the market instrument");

            tinkoffInstrument = instrument;
            tinkoffOrderbook = orderbook;
            instrumentType = type;
        }

        public string Figi => tinkoffInstrument.Figi;

        public string Ticker => tinkoffInstrument.Ticker;

        public string Isin => tinkoffInstrument.Isin;

        public InstrumentType Type => instrumentType;

        public string Name => tinkoffInstrument.Name;

        public Currency Currency => tinkoffInstrument.Currency;

        public int Lot => tinkoffInstrument.Lot;

        public decimal Price => tinkoffOrderbook.LastPrice;
    }
}
