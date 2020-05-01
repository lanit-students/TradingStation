using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DTO.MarketBrokerObjects;
using System.Threading.Tasks;
using Interfaces;
using DTO.BrokerRequests;
using DTO.RestRequests;
using System;
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
                [FromQuery] InstrumentType instrument
            )
        {
            return await command.Execute(
                new GetInstrumentsRequest()
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


    }
}