using System.Collections.Generic;
using BrokerServices;
using BrokerServices.Utils;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrokerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        private readonly ILogger<BrokerController> logger;

        public BrokerController([FromServices] ILogger<BrokerController> logger)
        {
            this.logger = logger;
        }

        [Route("getAllCurrencies")]
        [HttpPost]
        public List<IMarketInstrument> GetAllCurrencies([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType,logger).GetAllCurrencies();
        }

        [Route("getCurrency")]
        [HttpPost]
        public IMarketInstrument GetCurrency([FromBody] BankType bankType, [FromBody] string idCurrency)
        {
            return BrokerFactory.Create(bankType,logger).GetCurrency(idCurrency);
        }

        [Route("getAllStocks")]
        [HttpPost]
        public List<IMarketInstrument> GetAllStocks([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType,logger).GetAllStocks();
        }

        [Route("getStock")]
        [HttpPost]
        public IMarketInstrument GetStock([FromBody] BankType bankType, [FromBody] string idStock)
        {
            return BrokerFactory.Create(bankType,logger).GetStock(idStock);
        }

        [Route("getAllBonds")]
        [HttpPost]
        public List<IMarketInstrument> GetAllBonds([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType,logger).GetAllBonds();
        }

        [Route("getBond")]
        [HttpPost]
        public IMarketInstrument GetBond([FromBody] BankType bankType, [FromBody] string idBond)
        {
            return BrokerFactory.Create(bankType,logger).GetBond(idBond);
        }
    }
}