
using DTO.MarketBrokerObjects;

namespace DTO.BrokerRequests
{
    public class InternalTradeRequest
    {
        public BrokerType Broker { get; set; }
        public string Token { get; set; }
        public OperationType Operation { get; set; }
        public string Figi { get; set; }
        public int Lots { get; set; }
        public decimal Price { get; set; }
    }
}
