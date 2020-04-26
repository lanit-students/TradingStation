using BrokerService.Interfaces;
using BrokerService.Utils;
using DTO;
using DTO.MarketBrokerObjects;
using System.Collections.Generic;


namespace BrokerService.Commands
{
    public class GetAllCurrenciesCommand : IGetAllCurrenciesCommand
    {
       public IEnumerable<Instrument> Execute(BrokerType broker, string token, int depth)
        {
            return BrokerFactory.Create(broker, token, depth).GetInstruments(InstrumentType.Currency);
        }
    }
}
