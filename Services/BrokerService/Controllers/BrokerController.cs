using System.Collections.Generic;
using BrokerServices;
using BrokerServices.Utils;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrokerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        [Route("getAllCurrencies")]
        [HttpGet]
        public List<IMarketInstrument> GetAllCurrencies([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType).GetAllCurrencies();
        }

        [Route("getCurrency")]
        [HttpGet]
        public IMarketInstrument GetCurrency([FromBody] BankType bankType, string idCurrency)
        {
            return BrokerFactory.Create(bankType).GetCurrency(idCurrency);
        }

        [Route("getAllStocks")]
        [HttpGet]
        public List<IMarketInstrument> GetAllStocks([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType).GetAllStocks();
        }

        [Route("getStock")]
        [HttpGet]
        public IMarketInstrument GetStock([FromBody] BankType bankType, string idStock)
        {
            return BrokerFactory.Create(bankType).GetStock(idStock);
        }

        [Route("getAllBonds")]
        [HttpGet]
        public List<IMarketInstrument> GetAllBonds([FromBody] BankType bankType)
        {
            return BrokerFactory.Create(bankType).GetAllBonds();
        }

        [Route("getBond")]
        [HttpGet]
        public IMarketInstrument GetBond([FromBody] BankType bankType, string idBond)
        {
            return BrokerFactory.Create(bankType).GetBond(idBond);
        }
    }
}
