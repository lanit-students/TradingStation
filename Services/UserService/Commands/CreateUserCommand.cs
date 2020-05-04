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
using Microsoft.Extensions.Logging;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IRequestClient<InternalCreateUserRequest> client;
        private readonly IValidator<CreateUserRequest> validator;
        private ILogger<CreateUserCommand> logger;

        public CreateUserCommand(
            [FromServices] IRequestClient<InternalCreateUserRequest> client,
            [FromServices] IValidator<CreateUserRequest> validator,
            [FromServices] ILogger<CreateUserCommand> logger)
        {
            this.client = client;
            this.validator = validator;
            this.logger = logger;
        }

        private async Task<bool> CreateUser(InternalCreateUserRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(CreateUserRequest request)
        {
            validator.ValidateAndThrow(request);

            string passwordHash = ShaHash.GetPasswordHash(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Birthday = request.Birthday,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            UserAvatar userAvatar = null;

            if (request.Avatar != null && request.AvatarExtension != null)
            {
                userAvatar = new UserAvatar
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Avatar = request.Avatar,
                    AvatarExtension = request.AvatarExtension
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

            try
            {
                var createUserResult = await CreateUser(internalCreateUserRequest);
                return createUserResult;
            }
            catch (BadRequestException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);

                var ex = new BadRequestException(errorData.Item3);
                logger.LogInformation(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }
        }
    }
}