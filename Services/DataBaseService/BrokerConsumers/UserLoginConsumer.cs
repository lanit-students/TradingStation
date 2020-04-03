using DTO;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace DatabaseService.BrokerConsumers
{
    public class UserLoginConsumer : IConsumer<UserEmailPassword>
    {
        private User GetUserFromDatabase(string userEmail)
        {
            // TODO: Implement asking the database here
            return new User
            {
                Id = Guid.NewGuid(),
                Email = "bla@bla.com",
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
