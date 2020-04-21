using DataBaseService.Repositories.Interfaces;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Kernel;

namespace DataBaseService.BrokerConsumers
{
    public class CreateUserConsumer : IConsumer<InternalCreateUserRequest>
    {
        private readonly IUserRepository userRepository;

        public CreateUserConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private bool CreateUser(InternalCreateUserRequest request)
        {
            userRepository.CreateUser(request.User, request.Credential.Email);
            if (request.UserAvatar != null)
            {
                userRepository.CreateUserAvatar(request.UserAvatar);
            }

            userRepository.CreateUserCredential(request.Credential);

            return true;
        }

        public async Task Consume(ConsumeContext<InternalCreateUserRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateUser, context.Message);

            await context.RespondAsync(response);
        }
    }
}