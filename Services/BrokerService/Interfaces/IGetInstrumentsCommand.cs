using DTO;
using System.Collections.Generic;

namespace BrokerService.Interfaces
{
    public interface IGetInstrumentsCommand
    {
        IEnumerable<Instrument> Execute(BankType bank, string token, int depth, string instrument);
    }
}
