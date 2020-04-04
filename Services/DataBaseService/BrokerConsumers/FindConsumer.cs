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
        private readonly IUserRepository _repository;

        private UserCredential GetUserCredentialById(Guid Id)
        {
            return _repository.GetUserCredentialById(Id);
        }

        public FindConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<InternalFindUserRequest> context)
        {
            await context.RespondAsync(GetUserCredentialById(context.Message.Id));
        }
    }
}
