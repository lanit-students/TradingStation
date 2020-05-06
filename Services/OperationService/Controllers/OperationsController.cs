using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OperationService.Interfaces;

namespace OperationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly ILogger<OperationsController> logger;

        public OperationsController([FromServices] ILogger<OperationsController> logger)
        {
            this.logger = logger;
        }

        [Route("instruments/get")]
        [HttpGet]
        public async Task<IEnumerable<Instrument>> GetInstruments(
            [FromServices] ICommand<GetInstrumentsRequest, IEnumerable<Instrument>> command,
            [FromQuery] BrokerType broker,
            [FromQuery] string token,
            [FromQuery] InstrumentType instrument,
            [FromQuery] int depth)
        {
            return await command.Execute(
                new GetInstrumentsRequest
                {
                    Broker = broker,
                    Token = token,
                    Depth = depth,
                    Type = instrument
                });
        }

        [Route("candles/get")]
        [HttpGet]
        public async Task<IEnumerable<Candle>> GetCandles(
            [FromServices] ICommand<GetCandlesRequest, IEnumerable<Candle>> command,
            [FromQuery] BrokerType broker,
            [FromQuery] string token,
            [FromQuery] string figi
        )
        {
            return await command.Execute(
                new GetCandlesRequest
                {
                    Broker = broker,
                    Token = token,
                    Figi = figi
                });
        }

        [Route("addbot")]
        [HttpPost]
        public async Task<bool> CreateBot([FromServices] IAddBotCommand command, [FromBody] CreateBotRequest request)
        {
            logger.LogInformation("Add bot request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("deletebot")]
        [HttpDelete]
        public async Task<bool> DeleteBot([FromServices] IAddBotCommand command, [FromBody] CreateBotRequest request)
        {
            logger.LogInformation("Add bot request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("runbot")]
        [HttpPost]
        public async Task<bool> RunBot([FromServices] IAddBotCommand command, [FromBody] CreateBotRequest request)
        {
            logger.LogInformation("Add bot request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("disablebot")]
        [HttpPost]
        public async Task<bool> DisableBot([FromServices] IAddBotCommand command, [FromBody] CreateBotRequest request)
        {
            logger.LogInformation("Add bot request received from GUI to UserService");
            return await command.Execute(request);
        }
    }
}