using DTO;
using Kernel;
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
        private readonly IValidator<UserEmailPassword> validator;

        public CreateUserCommand([FromServices] IBus busControl, [FromServices] IValidator<UserEmailPassword> validator)
        {
            this.busControl = busControl;
            this.validator = validator;
        }

        private async Task<User> CreateUserInDataBaseService(UserEmailPassword data)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseServiceCreate");

            var client = busControl.CreateRequestClient<UserEmailPassword>(uri).Create(data);

            var response = await client.GetResponse<User>();

            return response.Message;
        }

        public async Task<Guid> Execute(UserEmailPassword data)
        {
            validator.ValidateAndThrow(data);

            User user = await CreateUserInDataBaseService(data);

            if (user.Email == null)
            {
                throw new Exception("Unable to create user =(");
            }

            return user.Id;
        }
    }
}

