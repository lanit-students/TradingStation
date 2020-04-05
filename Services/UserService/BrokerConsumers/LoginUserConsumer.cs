using DTO;
using DTO.RestRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UserService.BrokerConsumers
{
    public class LoginUserConsumer : IConsumer<LoginRequest>
    {
        private readonly IBus bus;

        public LoginUserConsumer([FromServices] IBus bus)
        {
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<LoginRequest> context)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = bus.CreateRequestClient<LoginRequest>(uri).Create(context.Message);

            var response = await client.GetResponse<UserCredential>();

            await context.RespondAsync(response.Message);
        }
    }
}
