using BrokerService.Interfaces;
using System.Collections.Generic;
using System;
using BrokerService.Utils;
using Kernel.CustomExceptions;
using DTO;

namespace BrokerService.Commands
{
    public class GetInstrumentsCommand : IGetInstrumentsCommand
    {
        public IEnumerable<Instrument> Execute(BankType bank, string token, int depth, string instrument)
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
