using DTO;
using System;
using System.Collections.Generic;

namespace OperationService.Bots.Utils
{
    public class PriceFiveMinutesBeforeTrigger : Trigger
    {
        private List<Candle> candles;

        public override event EventHandler<string> Triggered;

        private void OnMessageReceived(Candle candle)
        {
            if (Check())
            {
                Triggered(this, candle.Figi);
            }
        }

        private bool Check()
        {
            return true;
        }
    }
}
