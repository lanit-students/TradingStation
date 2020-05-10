using DTO.MarketBrokerObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class InstrumentData
    {
        public string Figi { get; set; }

        public string Name { get; set; }

        public BrokerType Broker { get; set; }

        public int Count { get; set; }

    }
}
