﻿using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;
using UserService.Validators;

namespace UserService.Commands
{
    public class EditUserCommand : IEditUserCommand
    {
        private readonly IBus busControl;
        private readonly IValidator<CreateUserRequest> validator;
        private readonly IValidator<string> validatorForPassword;
        private readonly IValidator<Guid> validatorForId;
        public EditUserCommand([FromServices] IBus busControl, [FromServices] IValidator<CreateUserRequest> validator,
            IValidator<string> validatorForPassword, IValidator<Guid> validatorForId)
        {
            this.busControl = busControl;
            this.validator = validator;
            this.validatorForPassword = validatorForPassword;
            this.validatorForId = validatorForId;
        }

        public async Task<bool> Execute(CreateUserRequest request, string newPassword, Guid id)
        {
            CreateUserRequestValidator createUserRequestValidator = new CreateUserRequestValidator();
            validator.ValidateAndThrow(request);
            validatorForPassword.ValidateAndThrow(newPassword);
            validatorForId.ValidateAndThrow(id);

            string passwordHash = ShaHash.GetPasswordHash(newPassword);

            if (newPassword == null)
            {
                throw new NotFoundException("Unable to edit user");
            }
            var user = new User
            {
                Id = id,
                Birthday = request.Birthday,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var credential = new UserCredential
            {
                Id = id,
                UserId = user.Id,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            var internalCreateUserRequest = new InternalCreateUserRequest
            {
                User = user,
                Credential = credential
            };

            var editUserResult = await EditUser(internalCreateUserRequest);
            if (!editUserResult)
            {
                throw new BadRequestException("Unable to edit user");
            }

            return editUserResult;
        }

        private async Task<bool> EditUser(InternalCreateUserRequest request)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = busControl.CreateRequestClient<InternalCreateUserRequest>(uri).Create(request);

            var response = await client.GetResponse<OperationResult>();

            return response.Message.IsSuccess;
        }

    }
}

