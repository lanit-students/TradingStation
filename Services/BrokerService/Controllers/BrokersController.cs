using System.Collections.Generic;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using BrokerService.Interfaces;
using Tinkoff.Trading.OpenApi.Models;
using DTO;

namespace BrokerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrokersController : ControllerBase
    {
        [Route("instruments/get")]
        [HttpGet]
        public IEnumerable<IMarketInstrument> GetInstruments([FromServices] IGetInstrumentsCommand command, [FromHeader] BankType bank, [FromHeader] string token, [FromHeader] int depth, [FromHeader] InstrumentType instrument)
        {
            return command.Execute(bank, token, depth, instrument);
        }
    }
}