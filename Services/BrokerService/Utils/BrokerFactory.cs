using DTO;
using Interfaces;
using Kernel.CustomExceptions;
using TinkoffIntegrationLib;
using Microsoft.Extensions.Logging;
using BrokerService.Controllers;

namespace BrokerServices.Utils
{
    public static class BrokerFactory
    {
        public static IBroker Create(BankType bankType, ILogger<BrokerController> logger, BrokerData brokerData)
        {
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