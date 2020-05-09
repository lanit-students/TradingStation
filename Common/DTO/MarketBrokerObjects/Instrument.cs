using DTO.MarketBrokerObjects;

namespace DTO
{
    public class Instrument
    {
        public virtual string Figi { get; set; }

        public virtual string Ticker { get; set; }

        public virtual string Isin { get; set; }

        public virtual InstrumentType Type { get; set; }

        public virtual string Name { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual int CountInLot { get; set; }

        public virtual decimal Price { get; set; }

        public virtual int TotalCount { get; set; }
    }
}
