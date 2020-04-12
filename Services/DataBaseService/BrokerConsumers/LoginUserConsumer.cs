using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
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
            await context.RespondAsync(GetUserCredential(context.Message.Email));
        }
    }
}
