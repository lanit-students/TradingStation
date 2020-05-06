using DTO.MarketBrokerObjects;

namespace DTO.BrokerRequests
{
    public class GetInstrumentsRequest
    {
        public BrokerType Broker { get; set; }

        public string Token { get; set; }

        public InstrumentType Type { get; set; }
    }
}
