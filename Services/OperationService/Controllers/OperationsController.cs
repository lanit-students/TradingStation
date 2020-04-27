using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DTO.MarketBrokerObjects;
using System.Threading.Tasks;
using Interfaces;
using DTO.BrokerRequests;
using DTO.RestRequests;
using System;

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
                [FromQuery] InstrumentType instrument
            )
        {
            return await command.Execute(
                new GetInstrumentsRequest()
                {
                    Broker = broker,
                    Token = token,
                    Type = instrument
                });
        }

        [Route("trade")]
        [HttpPost]
        public async Task <bool> Trade (
            [FromServices] ICommand<TradeRequest, bool> command,
            [FromBody] TradeRequest request
            )
        {
            return await command.Execute(request);
        }

        [Route("instruments/getFromPortfolio")]
        [HttpGet]
        public async Task<Instrument> GetInstrumentFromPortfolio(
            [FromServices] ICommand<GetInstrumentFromPortfolioRequest, Instrument> command,
            [FromQuery] string userId,
            [FromQuery] string figi
            )
        {
            return await command.Execute(
               new GetInstrumentFromPortfolioRequest()
               {
                   UserId = Guid.Parse(userId),
                   Figi = figi
               });
        }
        
    }
}