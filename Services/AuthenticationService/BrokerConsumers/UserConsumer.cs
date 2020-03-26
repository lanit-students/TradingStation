using DTO;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.BrokerConsumers
{
    public class UserConsumer : IConsumer<User>
    {
        public async Task Consume(ConsumeContext<User> context)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "bla@bla.com",
                Login = "bla",
                PasswordHash = "some_pass_hash"
            };

            await context.Publish(user);
        }
    }
}
