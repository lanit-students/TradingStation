using DTO.BrokerRequests;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using DTO;

namespace UserService.Commands
{
    public class GetUserByIdCommand : IGetUserByIdCommand
    {
        private readonly IBus busControl;

        public GetUserByIdCommand([FromServices]IBus busControl)
        {
            this.busControl = busControl;
        }

        private async Task<User> GetUserById(InternalGetUserByIdRequest request)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = busControl.CreateRequestClient<InternalGetUserByIdRequest>(uri).Create(request);

            var response = await client.GetResponse<User>();

            return response.Message;
        }

        public async Task<User> Execute(Guid request)
        {
            var internalRequest = new InternalGetUserByIdRequest { UserId = request };

            var user = await GetUserById(internalRequest);

            return user;
        }
    }
}
