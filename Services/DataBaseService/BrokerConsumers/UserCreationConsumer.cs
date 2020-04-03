using DTO;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class UserCreationConsumer : IConsumer<UserEmailPassword>
    {
        private User CreateUserInDatabase(UserEmailPassword userEmail)
        {
            // TODO: Implement asking the database here
            return new User
            {
                Id = Guid.NewGuid(),
                Email = "email",
                PasswordHash = "xxxxx"
            };
        }

        public async Task Consume(ConsumeContext<UserEmailPassword> context)
        {
            var creationResult = CreateUserInDatabase(context.Message);

            await context.RespondAsync(creationResult);
        }
    }
}
