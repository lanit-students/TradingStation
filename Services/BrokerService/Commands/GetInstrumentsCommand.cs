using BrokerService.Interfaces;
using System.Collections.Generic;
using System;
using BrokerService.Utils;
using Kernel.CustomExceptions;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrokerService.Commands
{
    public class GetInstrumentsCommand : IGetInstrumentsCommand
    {
        private ILogger<GetInstrumentsCommand> logger;

        public GetInstrumentsCommand([FromServices] ILogger<GetInstrumentsCommand> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Instrument> Execute(BankType bank, string token, int depth, string instrument)
        {
            try
            {
                logger.LogInformation($"Received request for getting {instrument} from {bank} with given depth {depth}.");
                return BrokerFactory.Create(bank, token, depth).GetInstruments(instrument);
            }
            catch (Exception)
            {
                var exception = new NotFoundException("Unable to get instruments.");
                logger.LogWarning(exception, $"Could not get {instrument} instrument from {bank} with given depth {depth}.");
                throw exception;
            }
        }
    }
}
