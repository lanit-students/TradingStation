using System;
using BrokerService.Utils;
using Interfaces;

namespace BrokerServices.Utils
{
    public static class BrokerFactory
    {
        public static IBroker Create(BankType bankType)
        {
            if (bankType == BankType.TinkoffBank) return 
        }
    }
}
