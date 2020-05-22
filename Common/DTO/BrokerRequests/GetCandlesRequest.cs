using DTO.MarketBrokerObjects;

namespace DTO.BrokerRequests
{
    public class GetCandlesRequest
    {
        public string Token { get; set; }

        public string Figi { get; set; }

        public int Interval { get; set; }

        public BrokerType Broker { get; set; }
    }
}