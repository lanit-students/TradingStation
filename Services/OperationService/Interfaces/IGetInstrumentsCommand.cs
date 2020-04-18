using DTO;
using DTO.MarketBrokerObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperationService.Interfaces
{
    public interface IGetInstrumentsCommand
    {
        Task<IEnumerable<Instrument>> Execute(BrokerType broker, string token, int depth, InstrumentType instrument);
    }
}
