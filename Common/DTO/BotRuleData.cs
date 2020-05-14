using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BotRuleData
    {
        public Guid Id { get; set; }

        public int OperationType { get; set; }

        public int TimeMarker { get; set; }

        public int TriggerValue { get; set; }
    }
}
