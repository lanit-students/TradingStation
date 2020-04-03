using DataBaseService.Interfaces;
using DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class UserCreationConsumer : IConsumer<UserEmailPassword>
    {
        private readonly IRepository<UserEmailPassword> userRepository;

        public UserCreationConsumer([FromServices] IRepository<UserEmailPassword> userRepository)
        {
            this.userRepository = userRepository;
        }

        private User CreateUserInDatabase(UserEmailPassword data)
        {
            try
            {
                return new User
                {
                    Id = userRepository.Create(data),
                    Email = data.Email,
                    PasswordHash = data.PasswordHash
                };
            }
            catch
            {
                return new User
                {
                    Email = null
                };
            }
        }

        public async Task Consume(ConsumeContext<UserEmailPassword> context)
        {
            var creationResult = CreateUserInDatabase(context.Message);

            await context.RespondAsync(creationResult);
        }
    }
}
