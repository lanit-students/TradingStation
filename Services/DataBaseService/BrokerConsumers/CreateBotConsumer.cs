using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class CreateBotConsumer : IConsumer<CreateBotRequest>
    {
        private readonly IUserRepository userRepository;

        public CreateBotConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private bool CreateBot(CreateBotRequest request)
        {
            return true;
        }

        public async Task Consume(ConsumeContext<CreateBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
