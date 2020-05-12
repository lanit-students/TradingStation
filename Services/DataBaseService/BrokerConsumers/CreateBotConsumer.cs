using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class CreateBotConsumer : IConsumer<CreateBotRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly ILogger logger;

        public CreateBotConsumer([FromServices] IBotRepository botRepository, [FromServices] ILogger<CreateBotConsumer> logger)
        {
            this.botRepository = botRepository;
            this.logger = logger;
        }

        private bool CreateBot(CreateBotRequest request)
        {
            logger.LogInformation("Create bot request received from OperationService");
            var bot = new BotData()
            {
                Name = request.Name,
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                IsRunning = false
            };
            botRepository.CreateBot(bot);
            // TODO add rules
            return true;
        }

        public async Task Consume(ConsumeContext<CreateBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
