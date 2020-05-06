using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class DeleteBotConsumer : IConsumer<DeleteBotRequest>
    {
        private readonly IBotRepository botRepository;

        public DeleteBotConsumer([FromServices] IBotRepository botRepository)
        {
            this.botRepository = botRepository;
        }

        private bool DeleteBot(DeleteBotRequest request)
        {
            return true;
        }

        public async Task Consume(ConsumeContext<DeleteBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(DeleteBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
