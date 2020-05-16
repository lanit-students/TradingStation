using System;

namespace DTO
{
    public class BotRuleData
    {
        public Guid Id { get; set; }

        public Guid BotId { get; set; }

        public int OperationType { get; set; }

        public int TimeMarker { get; set; }

        public int TriggerValue { get; set; }
    }
}
