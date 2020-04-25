using System;

namespace DTO
{
    public class Candle
    {
        public virtual decimal Open { get; }

        public virtual decimal Close { get; }

        public virtual decimal High { get; }

        public virtual decimal Low { get; }

        public virtual decimal Volume { get; }

        public virtual DateTime Time { get; }

        public virtual string Figi { get; }

        public bool isValid { get; set; }
    }
}