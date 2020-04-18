using System.Collections.Generic;
using BrokerService.Controllers;
using BrokerServices;
using BrokerServices.Utils;
using DTO;
using Interfaces;
using Microsoft.Extensions.Logging;
using Kernel.CustomExceptions;
using BrokerService.Interfaces;

namespace BrokerService.Commands
{
    public class GetImarketInstrumentCommand : IGetImarketInstrumentCommand
    {
        public List<IMarketInstrument> Execute(CommandsType commandType,BankType bankType,ILogger<BrokerController> logger,BrokerData brokerData)
        {
            switch (commandType)
            {
                case CommandsType.GetAllCurrencies:
                    return BrokerFactory.Create(bankType, logger, brokerData).GetAllCurrencies();
                case CommandsType.GetAllStocks:
                    return BrokerFactory.Create(bankType, logger, brokerData).GetAllStocks();
                case CommandsType.GetAllBonds:
                    return BrokerFactory.Create(bankType, logger, brokerData).GetAllBonds();
                default:
                    throw new BadRequestException("We cannot execute this command.");
            }
        }
        public IMarketInstrument Execute(CommandsType commandsType,BankType bankType,ILogger<BrokerController> logger,BrokerData brokerData,string Id)
        {
            switch (commandsType)
            {
                case CommandsType.GetCurrency:
                    return BrokerFactory.Create(bankType, logger, brokerData).GetCurrency(Id);
                case CommandsType.GetStock:
                    return BrokerFactory.Create(bankType, logger, brokerData).GetStock(Id);
                case CommandsType.GetBond:
                    return BrokerFactory.Create(bankType, logger, brokerData).GetBond(Id);
                default:
                    throw new BadRequestException("We cannot execute this command.");
            }
        }
    }
}
