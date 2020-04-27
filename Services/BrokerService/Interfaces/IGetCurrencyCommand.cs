using DTO;
using DTO.MarketBrokerObjects;

namespace BrokerService.Interfaces
{
    public interface IGetCurrencyCommand
    {
        Instrument Execute(BrokerType broker, string token, int depth, string currency);
    }
}
