using BrokerService;
using DTO;
using Interfaces;
using Kernel.CustomExceptions;
using TinkoffIntegrationLib;
using Microsoft.Extensions.Logging;
using BrokerService.Controllers;

namespace BrokerServices.Utils
{
    internal static class BrokerFactory
    {
        internal static IBroker Create(BankType bankType, ILogger<BrokerController> logger)
        {
            var brokerData = new CreateBrokerData();

            switch (bankType)
            {
                case BankType.TinkoffBank:
                    logger.LogInformation("Broker successfully created.");
                    return new TinkoffBankBroker(brokerData);
                default:
                    logger.LogInformation("Broker not created.");
                    throw new BadRequestException("We can't do it with this bank.");
            }   
        }
    }
}