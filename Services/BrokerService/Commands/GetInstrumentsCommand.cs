using Interfaces;
using BrokerService.Interfaces;
using System.Collections.Generic;
using Tinkoff.Trading.OpenApi.Models;
using System;
using BrokerService.Utils;
using BrokerService;
using Kernel.CustomExceptions;
using DTO;

namespace BrokerService.Commands
{
    public class GetInstrumentsCommand : IGetInstrumentsCommand
    {
        public IEnumerable<IMarketInstrument> Execute(BankType bank, string token, int depth, InstrumentType instrument)
        {
            try
            {
                return BrokerFactory.Create(bank, token, depth).GetInstruments(instrument);
            }
            catch (Exception)
            {
                throw new NotFoundException("Unable to get instruments.");
            }
        }
    }
}
