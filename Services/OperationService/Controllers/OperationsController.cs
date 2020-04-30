using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                new GetInstrumentsRequest
                {
                    Broker = broker,
                    Token = token,
                    Depth = depth,
                    Type = instrument
                });
        }

        [Route("candles/get")]
        [HttpGet]
        public async Task<IEnumerable<Candle>> GetCandles(
            [FromServices] ICommand<GetCandlesRequest, IEnumerable<Candle>> command,
            [FromQuery] BrokerType broker,
            [FromQuery] string token,
            [FromQuery] string figi
        )
        {
            return await command.Execute(
                new GetCandlesRequest
                {
                    Broker = broker,
                    Token = token,
                    Figi = figi
                });
        }
    }
}