using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokerService.Interfaces;
using DTO;
using DTO.MarketBrokerObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrokerService.Controllers
{
    [Route("[controller]")]
    public class BrokerController : Controller
    {
        [Route("getcurrency")]
        [HttpPost]
        public Instrument GetCurrency(
            [FromServices] IGetCurrencyCommand command,
            [FromBody] int depth,
            [FromBody] BrokerType broker,
            [FromBody] string token,
            [FromBody] string currency)
        {
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
            return command.Execute(broker, token, depth);       
        }
    }
}
