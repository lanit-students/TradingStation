using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BrokerService.Interfaces;
using DTO;

namespace BrokerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrokersController : ControllerBase
    {
        [Route("instruments/get")]
        [HttpGet]
        public IEnumerable<Instrument> GetInstruments(
                [FromServices] IGetInstrumentsCommand command,
                [FromQuery] BankType bank,
                [FromQuery] string token,
                [FromQuery] int depth,
                [FromQuery] string instrument)
        {
            return command.Execute(bank, token, depth, instrument);
        }
    }
}