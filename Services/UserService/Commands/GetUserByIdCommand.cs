using DTO.BrokerRequests;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using DTO;
using Kernel;

namespace UserService.Commands
{
    public class GetUserByIdCommand : IGetUserByIdCommand
    {
        private readonly IRequestClient<InternalGetUserByIdRequest> client;

        public GetUserByIdCommand([FromServices]IRequestClient<InternalGetUserByIdRequest> client)
        {
            this.client = client;
        }

        private async Task<User> GetUserById(InternalGetUserByIdRequest request)
        {
            var response = await client.GetResponse<OperationResult<User>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<User> Execute(Guid request)
        {
            var internalRequest = new InternalGetUserByIdRequest { UserId = request };

            var user = await GetUserById(internalRequest);

            return user;
        }
    }
}
