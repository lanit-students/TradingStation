using System.Collections.Generic;
using Interfaces;
using Kernel.CustomExceptions;
using BrokerService.Interfaces;

namespace BrokerService.Commands
{
    public class GetImarketInstrumentCommand : IGetImarketInstrumentCommand
    {
        public List<IMarketInstrument> Execute(CommandsType commandType,IBroker broker)
        {
            switch (commandType)
            {
                case CommandsType.GetAllCurrencies:
                    return broker.GetAllCurrencies();
                case CommandsType.GetAllStocks:
                    return broker.GetAllStocks();
                case CommandsType.GetAllBonds:
                    return broker.GetAllBonds();
                default:
                    throw new BadRequestException("We cannot execute this command.");
            }
        }
        public IMarketInstrument Execute(CommandsType commandsType,IBroker broker,string Id)
        {
            switch (commandsType)
            {
                case CommandsType.GetCurrency:
                    return broker.GetCurrency(Id);
                case CommandsType.GetStock:
                    return broker.GetStock(Id);
                case CommandsType.GetBond:
                    return broker.GetBond(Id);
                default:
                    throw new BadRequestException("We cannot execute this command.");
            }
        }
    }
}
