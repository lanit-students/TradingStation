using System;

namespace OperationService.Bots.Utils
{
    public abstract class Trigger
    {
        public virtual event EventHandler<TriggerEventArgs> Triggered;

        public abstract void Disable();
    }
}
