using System;
using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            
            if (request.UserAvatar == null)
            {
                request.User.UserAvatarId = Guid.Parse("6FF619FF-8B86-D011-B42D-00C04FC964FF");
            }
            else
            {
                userRepository.CreateUserAvatar(request.UserAvatar);
            }
            userRepository.CreateUser(request.User, request.Credential.Email);
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
