using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DTO.MarketBrokerObjects;
using System.Threading.Tasks;
using Interfaces;
using DTO.BrokerRequests;

namespace OperationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        [Route("instruments/get")]
        [HttpGet]
        public async Task<IEnumerable<Instrument>> GetInstruments(
                [FromServices] ICommand<GetInstrumentsRequest, IEnumerable<Instrument>> command,
                [FromQuery] BrokerType broker,
                [FromQuery] string token,
                [FromQuery] InstrumentType instrument,
                [FromQuery] int depth)
        {
            return await command.Execute(
                new GetInstrumentsRequest()
                {
                    Broker = broker,
                    Token = token,
                    Depth = depth,
                    Type = instrument
                });
        }

        [Route("instrument/get")]
        [HttpGet]
        public async void SubscribeOnCandle(
            [FromServices] ICommand<SubscribeOnCandleRequest, OperationResult> command,
            [FromQuery] BrokerType broker,
            [FromQuery] string token,
            [FromQuery] string Figi)
        {
            await command.Execute(
                new SubscribeOnCandleRequest()
                {
                    Broker = broker,
                    Token = token,
                    Figi = Figi
                });
        }
    }
}