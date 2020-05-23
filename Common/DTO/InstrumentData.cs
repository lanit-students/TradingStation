using DTO.MarketBrokerObjects;

namespace DTO
{
    public class InstrumentData
    {
        public string Figi { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }

        public BrokerType Broker { get; set; }

        public int Count { get; set; }
    }
}
