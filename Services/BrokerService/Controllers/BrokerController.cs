using System.Collections.Generic;
using BrokerServices;
using DTO;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BrokerService.Interfaces;
using BrokerService.Commands;
using BrokerServices.Utils;

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
            var broker = BrokerFactory.Create(bankType, logger, brokerData);
            return command.Execute(CommandsType.GetAllCurrencies, broker);
        }

        [Route("getCurrency")]
        [HttpPost]
        public IMarketInstrument GetCurrency([FromBody] BankType bankType, [FromBody] string idCurrency, [FromBody] BrokerData brokerData)
        {
            var broker = BrokerFactory.Create(bankType, logger, brokerData);
            return command.Execute(CommandsType.GetCurrency, broker, idCurrency);
        }

        [Route("getAllStocks")]
        [HttpPost]
        public List<IMarketInstrument> GetAllStocks([FromBody] BankType bankType, [FromBody] BrokerData brokerData)
        {
            var broker = BrokerFactory.Create(bankType, logger, brokerData);
            return command.Execute(CommandsType.GetAllCurrencies, broker);
        }

        [Route("getStock")]
        [HttpPost]
        public IMarketInstrument GetStock([FromBody] BankType bankType, [FromBody] string idStock, [FromBody] BrokerData brokerData)
        {
            var broker = BrokerFactory.Create(bankType, logger, brokerData);
            return command.Execute(CommandsType.GetCurrency, broker, idStock);
        }

        [Route("getAllBonds")]
        [HttpPost]
        public List<IMarketInstrument> GetAllBonds([FromBody] BankType bankType, [FromBody] BrokerData brokerData)
        {
            var broker = BrokerFactory.Create(bankType, logger, brokerData);
            return command.Execute(CommandsType.GetAllCurrencies, broker);
        }

        [Route("getBond")]
        [HttpPost]
        public IMarketInstrument GetBond([FromBody] BankType bankType, [FromBody] string idBond, [FromBody] BrokerData brokerData)
        {
            var broker = BrokerFactory.Create(bankType, logger, brokerData);
            return command.Execute(CommandsType.GetCurrency, broker, idBond);
        }
    }
}