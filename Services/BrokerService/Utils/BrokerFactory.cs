using System;
using DTO;
using Interfaces;
using TinkoffIntegrationLib;

namespace BrokerServices.Utils
{
    public static class BrokerFactory
    {
        public static IBroker Create(BankType bankType)
        {
            var brokerData = new CreateBrokerData();
            //token and depth?
            if (bankType == BankType.TinkoffBank) return new TinkoffBankBroker(brokerData);
            //Exception?
            throw new Exception("Bank is not found.");
        }
    }
}
