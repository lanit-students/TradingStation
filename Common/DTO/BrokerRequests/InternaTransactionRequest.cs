using System;

namespace DTO.BrokerRequests
{
    public class InternaTransactionRequest
    {
        public Guid Id { get; set; }
        public InternalTradeRequest Trade { get; set; }
    }
}
