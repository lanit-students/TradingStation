using DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UserService.BrokerConsumers
{
    public class UserLoginConsumer : IConsumer<UserEmailPassword>
    {
        private readonly IBus bus;

        public UserLoginConsumer([FromServices] IBus bus)
        {
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<UserEmailPassword> context)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseServiceLogin");

            var client = bus.CreateRequestClient<UserEmailPassword>(uri).Create(context.Message);

            var response = await client.GetResponse<User>();

            await context.RespondAsync(response.Message);
        }
    }
}
