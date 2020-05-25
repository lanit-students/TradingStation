using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BrokerRequests
{
    public class InternalEditBotRequest
    {
       public BotData Bot { get; set; }

       public BotRuleData Rule { get; set; }
    }
}
