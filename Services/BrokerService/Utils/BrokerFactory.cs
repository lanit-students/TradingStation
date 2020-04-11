using BrokerService;
using DTO;
using Interfaces;
using Kernel.CustomExceptions;
using TinkoffIntegrationLib;

namespace BrokerServices.Utils
{
    static class BrokerFactory
    {
        internal static IBroker Create(BankType bankType)
        {
            var brokerData = new CreateBrokerData
            {
                Depth = Constants.Depth,
                Token = Constants.Token
            };
            if (bankType == BankType.TinkoffBank) return new TinkoffBankBroker(brokerData);
            throw new BadRequestException("We can't do it with this bank.");
        }
    }
}