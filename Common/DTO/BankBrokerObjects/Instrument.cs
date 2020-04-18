namespace DTO
{
    /// <summary>
    /// Represents market instrument
    /// </summary>
    public class Instrument
    {
        public virtual string Figi { get; set; }

        public virtual string Ticker { get; set; }

        public virtual string Isin { get; set; }

        public virtual string Type { get; set; }

        public virtual string Name { get; set; }

        public virtual string Currency { get; set; }

        public virtual int Lot { get; set; }

        public virtual decimal Price { get; set; }
    }
}
