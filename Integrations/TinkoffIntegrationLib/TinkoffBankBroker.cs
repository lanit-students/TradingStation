using System;
using System.Collections.Generic;
using DTO;
using Interfaces;

namespace TinkoffIntegrationLib
{
    public class TinkoffBankBroker : IBroker
    {
        public List<ExchangeRate> GetExchangeRates()
        {
            throw new NotImplementedException();
        }
    }
}
