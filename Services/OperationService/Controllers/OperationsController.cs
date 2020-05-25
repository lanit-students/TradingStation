using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

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
            [FromQuery] InstrumentType instrument)
        {
            return await command.Execute(
                new GetInstrumentsRequest
                {
                    Broker = broker,
                    Token = token,
                    Type = instrument
                });
        }

        [Route("trade")]
        [HttpPost]
        public async Task <bool> Trade (
            [FromServices] ICommand<TradeRequest, bool> command,
            [FromBody] TradeRequest request
            )
        {
            logger.LogInformation($"Trade request of user {request.UserId} received");
            return await command.Execute(request);
        }

        [Route("instrument/getFromPortfolio")]
        [HttpGet]
        public async Task<Instrument> GetInstrumentFromPortfolio(
            [FromServices] ICommand<GetInstrumentFromPortfolioRequest, Instrument> command,
            [FromQuery] string userId,
            [FromQuery] string figi
            )
        {
            logger.LogInformation($"Get instrument {figi} from portfolio of user {userId} received");
            return await command.Execute(
               new GetInstrumentFromPortfolioRequest()
               {
                   UserId = Guid.Parse(userId),
                   Figi = figi
               });
        }

        [Route("getportfolio")]
        [HttpGet]
        public async Task<List<InstrumentData>> GetPortfolio(
            [FromServices] ICommand<GetPortfolioRequest, List<InstrumentData>> command,
            [FromHeader] Guid userId
            )
        {
            return await command.Execute(new GetPortfolioRequest() { UserId = userId });
        }

        [Route("userBalance/get")]
        [HttpGet]
        public async Task<UserBalance> GetUserBalance(
            [FromServices] ICommand<GetUserBalanceRequest, UserBalance> command,
            [FromQuery] Guid userId
            )
        {
            logger.LogInformation($"Get user {userId} balance  received");
            return await command.Execute(new GetUserBalanceRequest() { UserId = userId });
        }

        [Route("userBalance/update")]
        [HttpPut]
        public async Task<bool> UpdateUserBalance(
            [FromServices] ICommand<UpdateUserBalanceRequest, bool> command,
            [FromBody] UpdateUserBalanceRequest request
            )
        {
            logger.LogInformation($"Update user {request.UserId} balance  received");
            return await command.Execute(request);
        }

        [Route("candles/get")]
        [HttpGet]
        public async Task<IEnumerable<Candle>> GetCandles(
            [FromServices] ICommand<GetCandlesRequest, IEnumerable<Candle>> command,
            [FromQuery] BrokerType broker,
            [FromQuery] string token,
            [FromQuery] string figi,
            [FromQuery] int interval
        )
        {
            return await command.Execute(
                new GetCandlesRequest
                {
                    Broker = broker,
                    Token = token,
                    Figi = figi,
                    Interval = interval
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
            logger.LogInformation("Delete bot request received from GUI to OperationService");
            return await command.Execute(request);
        }

        [Route("bot/run")]
        [HttpPut]
        public async Task<bool> RunBot([FromServices] ICommand<RunBotRequest, bool> command, [FromBody] RunBotRequest request)
        {
            logger.LogInformation("Run bot request received from GUI to OperationService");
            return await command.Execute(request);
        }

        [Route("bot/disable")]
        [HttpPut]
        public async Task<bool> DisableBot([FromServices] ICommand<DisableBotRequest, bool> command, [FromBody] DisableBotRequest request)
        {
            logger.LogInformation("Disable bot request received from GUI to OperationService");
            return await command.Execute(request);
        }

        [Route("bot/get")]
        [HttpGet]
        public async Task<List<BotData>> GetBot([FromServices] ICommand<Guid, List<BotData>> command, [FromHeader] Guid userId)
        {
            logger.LogInformation("Get bots request received from GUI to OperationService");
            var result = await command.Execute(userId);
            return result;
        }

        [Route("bot/edit")]
        [HttpPost]
        public async Task<bool> EditBot([FromServices] ICommand<EditBotRequest, bool> command, [FromBody] EditBotRequest request)
        {
            logger.LogInformation("Edit bots request received from GUI to OperationService");
            var result = await command.Execute(request);
            return result;
        }

        [Route("transactions/get")]
        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetTransactions(
            [FromServices] ICommand<GetUserTransactionsRequest, IEnumerable<Transaction>> command,
            [FromQuery] Guid userId
        )
        {
            return await command.Execute(
                new GetUserTransactionsRequest
                {
                    UserId = userId
                });
        }
    }
}