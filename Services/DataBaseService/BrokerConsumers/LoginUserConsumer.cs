using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class LoginUserConsumer : IConsumer<InternalLoginRequest>
    {
        private readonly IUserRepository _repository;

        private UserCredential GetUserCredential(string email)
        {
            return _repository.GetUserCredential(email);
        }

        public LoginUserConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<InternalLoginRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetUserCredential, context.Message.Email);

            await context.RespondAsync(response);
        }
    }
}
