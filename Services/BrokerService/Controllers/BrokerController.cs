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
        [HttpGet]
        public List<IMarketInstrument> GetAllCurrencies([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType,logger).GetAllCurrencies();
        }

        [Route("getCurrency")]
        [HttpGet]
        public IMarketInstrument GetCurrency([FromBody] BankType bankType, string idCurrency)
        {
            return BrokerFactory.Create(bankType,logger).GetCurrency(idCurrency);
        }

        [Route("getAllStocks")]
        [HttpGet]
        public List<IMarketInstrument> GetAllStocks([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType,logger).GetAllStocks();
        }

        [Route("getStock")]
        [HttpGet]
        public IMarketInstrument GetStock([FromBody] BankType bankType, string idStock)
        {
            return BrokerFactory.Create(bankType,logger).GetStock(idStock);
        }

        [Route("getAllBonds")]
        [HttpGet]
        public List<IMarketInstrument> GetAllBonds([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType,logger).GetAllBonds();
        }

        [Route("getBond")]
        [HttpGet]
        public IMarketInstrument GetBond([FromBody] BankType bankType, string idBond)
        {
            return BrokerFactory.Create(bankType,logger).GetBond(idBond);
        }
    }
}