using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DatabaseService.BrokerConsumers
{
    public class LoginConsumer : IConsumer<LoginRequest>
    {
        private readonly IUserRepository _repository;

        private UserCredential GetUserCredential(string email)
        {
            return _repository.GetUserCredential(email);
        }

        public LoginConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<LoginRequest> context)
        {
            await context.RespondAsync(GetUserCredential(context.Message.Email));
        }
    }
}
