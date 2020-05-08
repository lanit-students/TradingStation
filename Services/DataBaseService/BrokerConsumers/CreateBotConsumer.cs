using DataBaseService.Repositories.Interfaces;
using DTO.Bots;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class CreateBotConsumer : IConsumer<CreateBotRequest>
    {
        private readonly IBotRepository botRepository;

        public CreateBotConsumer([FromServices] IBotRepository botRepository)
        {
            this.botRepository = botRepository;
        }

        private bool CreateBot(CreateBotRequest request)
        {
            var bot = new Bot()
            {
                Name = request.Name,
                ID = Guid.NewGuid(),
                UserID = request.UserId,
                Rules = new List<BotRule>(),
                isActive = false
            };
            botRepository.CreateBot(bot);
            return true;
        }

        public async Task Consume(ConsumeContext<CreateBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
