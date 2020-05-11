using System;

namespace OperationService.Bots.Utils
{
    abstract class Trigger
    {
        public Guid Id { get; set; }

        public TriggerType Type { get; set; }

        public int TriggerValue { get; set; }

        public abstract bool Check(string figi);
    }
}
