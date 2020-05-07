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
using UserService.Utils;
using Microsoft.Extensions.Logging;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IRequestClient<InternalCreateUserRequest> client;
        private readonly IValidator<CreateUserRequest> validator;
        private readonly ISecretTokenEngine secretTokenEngine;
        private readonly ILogger<CreateUserCommand> logger;
        private readonly IEmailSender emailSender;


        public CreateUserCommand(
            [FromServices] IRequestClient<InternalCreateUserRequest> client,
            [FromServices] IValidator<CreateUserRequest> validator,
            [FromServices] ISecretTokenEngine secretTokenEngine,
            [FromServices] ILogger<CreateUserCommand> logger,
            [FromServices] IEmailSender emailSender)
        {
            this.client = client;
            this.validator = validator;
            this.secretTokenEngine = secretTokenEngine;
            this.logger = logger;
            this.emailSender = emailSender;
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

            var createUserResult = await CreateUser(internalCreateUserRequest);

            if (!createUserResult)
            {
                var e = new BadRequestException("Unable to create user");
                logger.LogWarning(e, "BadRequest thrown while trying to create User.");
                throw e;
            }

            emailSender.SendEmail(request.Email,secretTokenEngine);

            return createUserResult;
        }
    }
}