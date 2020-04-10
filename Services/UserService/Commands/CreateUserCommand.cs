using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using FluentValidation;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IBus busControl;
        private readonly IValidator<CreateUserRequest> validator;

        public CreateUserCommand([FromServices] IBus busControl, [FromServices] IValidator<CreateUserRequest> validator)
        {
            this.busControl = busControl;
            this.validator = validator;
        }

        private async Task<bool> CreateUser(InternalCreateUserRequest request)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = busControl.CreateRequestClient<InternalCreateUserRequest>(uri).Create(request);

            var response = await client.GetResponse<OperationResult>();

            return response.Message.IsSuccess;
        }

        public async Task<bool> Execute(CreateUserRequest request)
        {
            validator.ValidateAndThrow(request);

            string passwordHash = ShaHash.GetPasswordHash(request.Password);
            User user = new User
            {
                Id = Guid.NewGuid(),
                Birthday = request.Birthday,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            UserAvatar userAvatar = null;
            if (request.Avatar != null && request.AvatarType != null)
            {
                user.UserAvatarId = Guid.NewGuid();
                userAvatar = new UserAvatar
                {
                    Id = user.UserAvatarId,
                    Avatar = request.Avatar,
                    TypeAvatar = request.AvatarType
                };
            }
            var credential = new UserCredential
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Email = request.Email,
                PasswordHash = passwordHash
            };
            
            var internalCreateUserRequest = new InternalCreateUserRequest
            {
                User = user,
                Credential = credential,
                UserAvatar = userAvatar
            };

            var createUserResult = await CreateUser(internalCreateUserRequest);
            if (!createUserResult)
            {
                throw new BadRequestException("Unable to create user");
            }

            return createUserResult;
        }
    }
}
