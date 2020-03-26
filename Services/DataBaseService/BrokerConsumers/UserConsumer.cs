using DTO;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace DatabaseService.BrokerConsumers
{
    public class UserConsumer : IConsumer<UserEmailPassword>
    {
        private User GetUserFromDatabase(string userEmail)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = "bla@bla.com",
                Login = "bla",
                PasswordHash = "some_pass_hash"
            };
        }

        public async Task Consume(ConsumeContext<UserEmailPassword> context)
        {
            var user = GetUserFromDatabase(context.Message.Email);

            await context.RespondAsync(user);
        }
    }
}
