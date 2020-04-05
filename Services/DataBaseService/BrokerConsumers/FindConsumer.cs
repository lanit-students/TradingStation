using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class FindConsumer : IConsumer<InternalFindUserRequest>
    {
        private readonly IUserRepository userRepository;

        private UserCredential GetUserCredentialById(Guid userCredentialId)
        {
            return userRepository.GetUserCredentialById(userCredentialId);
        }

        public FindConsumer([FromServices] IUserRepository repository)
        {
            userRepository = repository;
        }

        public async Task Consume(ConsumeContext<InternalFindUserRequest> context)
        {
            await context.RespondAsync(GetUserCredentialById(context.Message.Id));
        }
    }
}
