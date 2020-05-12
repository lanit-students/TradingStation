using System;

namespace OperationService.Bots.Utils
{
    public abstract class Trigger
    {
        public Guid Id { get; set; }

        public int TimeMarker { get; set; }

        public int TriggerValue { get; set; }

        public abstract bool Check(string figi);
    }
}
