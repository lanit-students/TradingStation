using DTO;
using Interfaces;
using System.Collections.Generic;
using Tinkoff.Trading.OpenApi.Models;

namespace BrokerService.Interfaces
{
    public interface IGetInstrumentsCommand
    {
        IEnumerable<IMarketInstrument> Execute(BankType bank, string token, int depth, InstrumentType instrument);
    }
}
