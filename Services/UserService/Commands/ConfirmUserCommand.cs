using DTO;
using DTO.BrokerRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class ConfirmUserCommand : IConfirmUserCommand
    {
        private readonly IRequestClient<InternalConfirmUserRequest> client;
        private readonly ISecretTokenEngine secretTokenEngine;
        private readonly ILogger<ConfirmUserCommand> logger;
        public ConfirmUserCommand([FromServices]IRequestClient<InternalConfirmUserRequest> client,
            [FromServices] ISecretTokenEngine secretTokenEngine, [FromServices] ILogger<ConfirmUserCommand> logger)
        {
            this.client = client;
            this.secretTokenEngine = secretTokenEngine;
            this.logger = logger;
        }

        private async Task<bool> UserConfirmation(InternalConfirmUserRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(Guid secretToken)
        {
            var request = new InternalConfirmUserRequest { UserEmail=secretTokenEngine.GetEmail(secretToken) };

            var confirmUserResult = await UserConfirmation(request);

            if (!confirmUserResult)
            {
                var e = new BadRequestException("Unable to confirm user");
                logger.LogWarning(e, "BadRequest thrown while trying to confirm User.");
                throw e;
            }
            return true;
        }
    }
}
