using DTO;
using Interfaces;
using Kernel.CustomExceptions;
using TinkoffIntegrationLib;

namespace BrokerService.Utils
{
    public static class BrokerFactory
    {
        public static IBroker Create(BankType bankType, string token, int depth = 10)
        {
            return bankType switch
            {
                BankType.TinkoffBank =>
                    new TinkoffBankBroker(token, depth),
                _ =>
                    throw new BadRequestException("Invalid bank type")
            };
        }
    }
}
