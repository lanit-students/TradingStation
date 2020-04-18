using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OperationService.Interfaces;
using DTO;
using DTO.MarketBrokerObjects;
using System.Threading.Tasks;

namespace OperationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        [Route("instruments/get")]
        [HttpGet]
        public async Task<IEnumerable<Instrument>> GetInstruments(
                [FromServices] IGetInstrumentsCommand command,
                [FromQuery] BrokerType broker,
                [FromQuery] string token,
                [FromQuery] InstrumentType instrument,
                [FromQuery] int depth)
        {
            return await command.Execute(broker, token, depth, instrument);
        }
    }
}