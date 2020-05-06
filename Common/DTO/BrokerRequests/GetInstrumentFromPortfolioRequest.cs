using System;

namespace DTO.BrokerRequests
{
    public class GetInstrumentFromPortfolioRequest
    {
        public string Figi { get; set; }
        public Guid UserId { get; set; }
    }
}

