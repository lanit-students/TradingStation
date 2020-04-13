using Tinkoff.Trading.OpenApi.Models;

namespace Interfaces
{
    /// <summary>
    /// Represents market instrument
    /// </summary>
    public interface IMarketInstrument
    {
        string Figi { get; }

        string Ticker { get; }

        string Isin { get; }

        InstrumentType Type { get; }

        string Name { get; }

        Currency Currency { get; }

        int Lot { get; }

        decimal Price { get; }
    }
}
