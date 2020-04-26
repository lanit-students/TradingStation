using DTO;
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
    public class ConfirmUserCommand : IConfirmUserCommand
    {
        private readonly IRequestClient<InternalConfirmUserRequest> client;

        public ConfirmUserCommand([FromServices]IRequestClient<InternalConfirmUserRequest> client)
        {
            this.client = client;
        }

        private async Task<bool> UserConfirmation(InternalConfirmUserRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(string secretToken)
        {
            var request = new InternalConfirmUserRequest { UserEmail=secretToken };

            var confirmUserResult = await UserConfirmation(request);

            if (!confirmUserResult)
            {
                throw new BadRequestException("Unable to confirm user.");
            }

            return confirmUserResult;
        }
    }
}
