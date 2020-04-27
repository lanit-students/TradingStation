using System.Collections.Generic;
using BrokerService.Interfaces;
using DTO;
using DTO.MarketBrokerObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrokerService.Controllers
{
    [Route("[controller]")]
    public class BrokerController : Controller
    {
        private readonly ILogger<BrokerController> logger;

        public BrokerController([FromServices] ILogger<BrokerController> logger)
        {
            this.logger = logger;
        }

        [Route("getcurrency")]
        [HttpPost]
        public Instrument GetCurrency(
            [FromServices] IGetCurrencyCommand command,
            [FromBody] int depth,
            [FromBody] BrokerType broker,
            [FromBody] string token,
            [FromBody] string currency)
        {
            logger.LogInformation("Success");

            return command.Execute(broker, token, depth, currency);
        }
        [Route("getallcurrencies")]
        [HttpPost]
        public IEnumerable<Instrument> GetAllCurrencies(
            [FromServices] IGetAllCurrenciesCommand command, 
            [FromBody] int depth,
            [FromBody] BrokerType broker,
            [FromBody] string token)
        {
            logger.LogInformation("Success");

            return command.Execute(broker, token, depth);       
        }
    }
}
