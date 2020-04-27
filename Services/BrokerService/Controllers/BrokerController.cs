using System.Collections.Generic;
using BrokerService.Interfaces;
using BrokerService.Utils;
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
            [FromBody] RequestInfo request,
            [FromHeader] string currency
            )
        {
            logger.LogInformation("Success");

            return command.Execute(request.broker, request.token, request.depth, currency);
        }
        [Route("getallcurrencies")]
        [HttpPost]
        public IEnumerable<Instrument> GetAllCurrencies(
            [FromServices] IGetAllCurrenciesCommand command,
            [FromBody] RequestInfo request)
        {
            logger.LogInformation("Success");

            return command.Execute(request.broker, request.token, request.depth);       
        }
    }
}
