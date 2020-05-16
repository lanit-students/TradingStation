using System;
using System.Collections.Generic;

namespace DTO.RestRequests
{
    public class CreateBotRequest
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public List<BotRuleData> Rules { get; set; }
    }
}
