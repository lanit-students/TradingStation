using System.Collections.Generic;
using BrokerServices;
using DTO;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BrokerService.Interfaces;
using BrokerService.Commands;

namespace BrokerService.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class BrokerController : ControllerBase
    {
        private readonly ILogger<BrokerController> logger;
        private IGetImarketInstrumentCommand command;

        public BrokerController([FromServices] ILogger<BrokerController> logger)
        {
            this.logger = logger;
            command = new GetImarketInstrumentCommand();
        }

        [Route("getAllCurrencies")]
        [HttpPost]
        public List<IMarketInstrument> GetAllCurrencies([FromBody] BankType bankType, [FromBody] BrokerData brokerData)
        {
            return command.Execute(CommandsType.GetAllCurrencies, bankType, logger, brokerData);
        }

        [Route("getCurrency")]
        [HttpPost]
        public IMarketInstrument GetCurrency([FromBody] BankType bankType, [FromBody] string idCurrency, [FromBody] BrokerData brokerData)
        {
            return command.Execute(CommandsType.GetCurrency, bankType, logger, brokerData, idCurrency);
        }

        [Route("getAllStocks")]
        [HttpPost]
        public List<IMarketInstrument> GetAllStocks([FromBody] BankType bankType, [FromBody] BrokerData brokerData)
        {
            return command.Execute(CommandsType.GetAllCurrencies, bankType, logger, brokerData);
        }

        [Route("getStock")]
        [HttpPost]
        public IMarketInstrument GetStock([FromBody] BankType bankType, [FromBody] string idStock, [FromBody] BrokerData brokerData)
        {
            return command.Execute(CommandsType.GetCurrency, bankType, logger, brokerData, idStock);
        }

        [Route("getAllBonds")]
        [HttpPost]
        public List<IMarketInstrument> GetAllBonds([FromBody] BankType bankType, [FromBody] BrokerData brokerData)
        {
            return command.Execute(CommandsType.GetAllCurrencies, bankType, logger, brokerData);
        }

        [Route("getBond")]
        [HttpPost]
        public IMarketInstrument GetBond([FromBody] BankType bankType, [FromBody] string idBond, [FromBody] BrokerData brokerData)
        {
            return command.Execute(CommandsType.GetCurrency, bankType, logger, brokerData, idBond);
        }
    }
}