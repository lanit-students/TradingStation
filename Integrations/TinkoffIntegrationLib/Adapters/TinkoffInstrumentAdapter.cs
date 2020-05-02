using System;
using DTO;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffIntegrationLib.Adapters
{
    public class TinkoffInstrumentAdapter : Instrument
    {
        private readonly MarketInstrument tinkoffInstrument;

        /*
         * If I will pass to ctor tinkoff context,
         * class obtain a lot of excess info
        */
        public TinkoffInstrumentAdapter(InstrumentType type, MarketInstrument instrument)
        {
            tinkoffInstrument = instrument;
            Type = (DTO.MarketBrokerObjects.InstrumentType)Enum.Parse(typeof(DTO.MarketBrokerObjects.InstrumentType), type.ToString());
        }

        public override string Figi => tinkoffInstrument.Figi;

        public override string Ticker => tinkoffInstrument.Ticker;

        public override string Isin => tinkoffInstrument.Isin;

        public override DTO.MarketBrokerObjects.InstrumentType Type { get; set; }

        public override string Name => tinkoffInstrument.Name;

        public override DTO.MarketBrokerObjects.Currency Currency 
            => (DTO.MarketBrokerObjects.Currency)Enum.
            Parse(typeof(DTO.MarketBrokerObjects.Currency),tinkoffInstrument.Currency.ToString());

        public override int CountInLot => tinkoffInstrument.Lot;
    }
}
