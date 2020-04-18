using DTO;
using Kernel.CustomExceptions;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffIntegrationLib
{
    public class TinkoffInstrumentAdapter : Instrument
    {
        private readonly MarketInstrument tinkoffInstrument;
        private readonly Orderbook tinkoffOrderbook;

        /*
         * If I will pass to ctor tinkoff context,
         * class obtain a lot of excess info
        */
        public TinkoffInstrumentAdapter(InstrumentType type, MarketInstrument instrument, Orderbook orderbook)
        {
            if (instrument.Figi != orderbook.Figi)
                throw new BadRequestException("Transfer orderbook does not match the market instrument");

            tinkoffInstrument = instrument;
            tinkoffOrderbook = orderbook;
            Type = type.ToString();
        }

        public override string Figi => tinkoffInstrument.Figi;

        public override string Ticker => tinkoffInstrument.Ticker;

        public override string Isin => tinkoffInstrument.Isin;

        public override string Type { get; set; }

        public override string Name => tinkoffInstrument.Name;

        public override string Currency => tinkoffInstrument.Currency.ToString();

        public override int Lot => tinkoffInstrument.Lot;

        public override decimal Price => tinkoffOrderbook.LastPrice;
    }
}
