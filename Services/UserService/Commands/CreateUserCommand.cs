using DTO;
using Kernel;
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

        private async Task<User> CreateUserInDataBaseService(UserEmailPassword data)
        {
            var uri = new Uri("rabbitmq://localhost/DataBaseServiceCreate");

            var client = busControl.CreateRequestClient<UserEmailPassword>(uri).Create(data);

            var response = await client.GetResponse<User>();

            return response.Message;
        }

        public async Task<Guid> Execute(UserEmailPassword data)
        {
            CommonValidations.ValidateEmail(data.Email);

            User user = await CreateUserInDataBaseService(data);

            if (user.Email == null)
            {
                throw new Exception("Unable to create user =(");
            }

            //throw new Exception("ololo");
            return user.Id;
        }
    }
}

