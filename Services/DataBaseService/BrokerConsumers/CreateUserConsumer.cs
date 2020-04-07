using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MassTransit;

using DTO;
using DTO.BrokerRequests;
using DataBaseService.Repositories.Interfaces;



namespace DataBaseService.BrokerConsumers
{
    public class CreateUserConsumer : IConsumer<InternalCreateUserRequest>
    {
        private readonly IUserRepository userRepository;

        public CreateUserConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private OperationResult CreateUser(InternalCreateUserRequest request)
        {
            userRepository.CreateUser(request.User);
            userRepository.CreateUserCredential(request.Credential);

            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<InternalCreateUserRequest> context)
        {
            var creationResult = CreateUser(context.Message);

            await context.RespondAsync(creationResult);
        }
    }
}
