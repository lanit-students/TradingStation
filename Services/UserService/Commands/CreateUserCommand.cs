﻿using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IBus busControl;

        public CreateUserCommand([FromServices] IBus busControl)
        {
            this.busControl = busControl;
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
            CommonValidations.ValidateEmail(request.Email);

            string passwordHash = ShaHash.GetPasswordHash(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Birthday = request.Birthday,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

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
                Credential = credential
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
