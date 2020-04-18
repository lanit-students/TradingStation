using System.Collections.Generic;
using BrokerService.Controllers;
using BrokerServices;
using DTO;
using Interfaces;
using Microsoft.Extensions.Logging;

namespace BrokerService.Interfaces
{
    interface IGetImarketInstrumentCommand
    {
        List<IMarketInstrument> Execute(CommandsType commandType, BankType bankType, ILogger<BrokerController> logger, BrokerData brokerData);
        IMarketInstrument Execute(CommandsType commandsType, BankType bankType, ILogger<BrokerController> logger, BrokerData brokerData, string Id);
    }
}
