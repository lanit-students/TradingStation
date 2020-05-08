using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [Route("bot/create")]
        [HttpPost]
        public async Task<bool> CreateBot([FromServices] ICommand<CreateBotRequest, bool> command, [FromBody] CreateBotRequest request)
        {
            logger.LogInformation("Create bot request received from GUI to OperationService");
            return await command.Execute(request);
        }

        [Route("bot/delete")]
        [HttpDelete]
        public async Task<bool> DeleteBot([FromServices] ICommand<DeleteBotRequest, bool> command, [FromBody] DeleteBotRequest request)
        {
            logger.LogInformation("Delete bot request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("bot/run")]
        [HttpPut]
        public async Task<bool> RunBot([FromServices] ICommand<RunBotRequest, bool> command, [FromBody] RunBotRequest request)
        {
            logger.LogInformation("Run bot request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("bot/disable")]
        [HttpPut]
        public async Task<bool> DisableBot([FromServices] ICommand<DisableBotRequest, bool> command, [FromBody] DisableBotRequest request)
        {
            logger.LogInformation("Disable bot request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("bot/get")]
        [HttpGet]
        public async Task<bool> GetBot([FromServices] ICommand<Guid, bool> command, [FromHeader] Guid userId)
        {
            logger.LogInformation("Get bots request received from GUI to UserService");
            return await command.Execute(userId);
        }
    }
}