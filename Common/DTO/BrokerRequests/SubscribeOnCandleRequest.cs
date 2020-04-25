using DTO.MarketBrokerObjects;

namespace DTO.BrokerRequests
{
    public class SubscribeOnCandleRequest
    {
        public string Token { get; set; }

        public string Figi { get; set; }

        public BrokerType Broker { get; set; }
    }
}