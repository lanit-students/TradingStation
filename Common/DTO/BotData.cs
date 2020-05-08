using System;
using System.Collections.Generic;

namespace DTO
{
    public class BotData
    {
        public Guid Id;
        public Guid UserId;
        public string Name;
        public List<BotRuleData> Rules;
        public bool IsRunning;
    }
}
