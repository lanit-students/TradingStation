using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
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
