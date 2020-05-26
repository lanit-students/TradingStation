using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BrokerRequests
{
    public class InternalEditBotRequest
    {
        public Guid BotId { get; set; }
        public string Name { get; set; }
        public List<BotRuleData> Rules { get; set; }
    }
}