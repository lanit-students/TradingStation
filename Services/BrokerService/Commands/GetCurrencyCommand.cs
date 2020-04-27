using BrokerService.Interfaces;
using BrokerService.Utils;
using DTO;
using DTO.MarketBrokerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerService.Commands
{
    public class GetCurrencyCommand : IGetCurrencyCommand
    {
        public Instrument Execute(BrokerType broker, string token, int depth, string currency)
        {
            return BrokerFactory.Create(broker, token, depth).GetInstruments(InstrumentType.Currency).FirstOrDefault(instrument => instrument.Currency == currency);
        }
    }
}
