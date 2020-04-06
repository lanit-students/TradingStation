using BrokerServices;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerService.Utils
{
    public class Broker : IBroker
    {
        BankType bankType;

        public Broker(BankType bankType)
        {
            this.bankType = bankType;
        }

        public int Depth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<IMarketInstrument> GetAllBonds()
        {
            throw new NotImplementedException();
        }

        public List<IMarketInstrument> GetAllCurrencies()
        {
            throw new NotImplementedException();
        }

        public List<IMarketInstrument> GetAllStocks()
        {
            throw new NotImplementedException();
        }

        public IMarketInstrument GetBond(string idBond)
        {
            throw new NotImplementedException();
        }

        public IMarketInstrument GetCurrency(string idCurrency)
        {
            throw new NotImplementedException();
        }

        public IMarketInstrument GetStock(string idStock)
        {
            throw new NotImplementedException();
        }
    }
}
