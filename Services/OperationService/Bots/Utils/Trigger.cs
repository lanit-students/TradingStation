using System;

namespace OperationService.Bots.Utils
{
    public abstract class Trigger
    {
        public Guid Id { get; set; }

        public int TimeMarker { get; set; }

        public int TriggerValue { get; set; }

        public virtual event EventHandler<string> Triggered;
    }
}
