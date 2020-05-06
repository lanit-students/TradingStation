using DTO.MarketBrokerObjects;
using Interfaces;
using Kernel.CustomExceptions;
using TinkoffIntegrationLib;

namespace BrokerService.Utils
{
    public static class BrokerFactory
    {
        public static IBroker Create(BrokerType broker, string token)
        {
            return broker switch
            {
                BrokerType.TinkoffBroker =>
                    new TinkoffBankBroker(token),
                _ =>
                    throw new BadRequestException("Invalid broker type")
            };
        }
    }
}
