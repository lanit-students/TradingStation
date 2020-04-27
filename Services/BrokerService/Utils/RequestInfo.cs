using DTO.MarketBrokerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerService.Utils
{
    public class RequestInfo
    {
        public int depth;
        public  BrokerType broker;
        public string token;

        public RequestInfo() { }

        public RequestInfo(int depth, BrokerType broker, string token)
        {
            this.depth = depth;
            this.broker = broker;
            this.token = token;
        }
    }
}
