using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.RestRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class LoginConsumer : IConsumer<InternalLoginRequest>
    {
        private readonly IUserRepository userRepository;

        private UserCredential GetUserCredential(string email)
        {
            return userRepository.GetUserCredential(email);
        }

        public LoginConsumer([FromServices] IUserRepository repository)
        {
            userRepository = repository;
        }

        public async Task Consume(ConsumeContext<InternalLoginRequest> context)
        {
            await context.RespondAsync(GetUserCredential(context.Message.Email));
        }
    }
}
