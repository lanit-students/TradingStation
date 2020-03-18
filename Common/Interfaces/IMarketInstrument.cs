using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    /// <summary>
    /// Represents market instrument 
    /// </summary>
    public interface IMarketInstrument
    {
        /// <summary>
        /// Figi e.g.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Name market instrument
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Price by market instrument
        /// </summary>
        decimal Price { get; }

    }
}
