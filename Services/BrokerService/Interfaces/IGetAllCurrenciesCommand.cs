using DTO;
using DTO.MarketBrokerObjects;
using System.Collections.Generic;


namespace BrokerService.Interfaces
{
    public interface IGetAllCurrenciesCommand
    {
        IEnumerable<Instrument> Execute(BrokerType broker, string token, int depth);
    }
}
