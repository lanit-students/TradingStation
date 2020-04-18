using DTO;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IBroker
    {
        IEnumerable<Instrument> GetInstruments(string type);

        /// <summary>
        /// Depth of market glass
        /// </summary>
        int Depth { get; set; }
    }
}
