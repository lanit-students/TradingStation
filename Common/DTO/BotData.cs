using System;
using System.Collections.Generic;

namespace DTO
{
    public class BotData
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<BotRuleData> Rules { get; set; }
        public bool IsRunning { get; set; }
    }
}
