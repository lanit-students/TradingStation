using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class RunBotConsumer : IConsumer<RunBotRequest>
    {
        private readonly IBotRepository botRepository;

        public RunBotConsumer([FromServices] IBotRepository botRepository)
        {
            this.botRepository = botRepository;
        }

        private bool RunBot(RunBotRequest request)
        {
            botRepository.Run(request.ID);
            return true;
        }

        public async Task Consume(ConsumeContext<RunBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(RunBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
