using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UserService.BrokerConsumers
{
    public class LoginUserConsumer : IConsumer<InternalLoginRequest>
    {
        private readonly IBus bus;

        public LoginUserConsumer([FromServices] IBus bus)
        {
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<InternalLoginRequest> context)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = bus.CreateRequestClient<InternalLoginRequest>(uri).Create(context.Message);

            var response = await client.GetResponse<UserCredential>();

            await context.RespondAsync(response.Message);
        }
    }
}
