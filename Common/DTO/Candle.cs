using System;

namespace DTO
{
    public class Candle
    {
        public virtual decimal Open { get; set; }

        public virtual decimal Close { get; set; }

        public virtual decimal High { get; set; }

        public virtual decimal Low { get; set; }

        public virtual decimal Volume { get; set; }

        public virtual DateTime Time { get; set; }

        public virtual string Figi { get; set; }
    }
}