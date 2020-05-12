using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class DeleteBotConsumer : IConsumer<DeleteBotRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly ILogger logger;

        public DeleteBotConsumer([FromServices] IBotRepository botRepository, [FromServices] ILogger<DeleteBotConsumer> logger)
        {
            this.botRepository = botRepository;
            this.logger = logger;
        }

        private bool DeleteBot(DeleteBotRequest request)
        {
            logger.LogInformation("Delete bot request received from OperationService");
            botRepository.DeleteBot(request.ID);
            return true;
        }

        public async Task Consume(ConsumeContext<DeleteBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(DeleteBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
