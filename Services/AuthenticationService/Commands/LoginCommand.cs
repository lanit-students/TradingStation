using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MassTransit;

using AuthenticationService.Interfaces;
using DTO;
using Kernel.CustomExceptions;
using Kernel;
using DTO.RestRequests;
using DTO.BrokerRequests;
using Microsoft.Extensions.Logging;
using System;

namespace AuthenticationService.Commands
{
    public class LoginCommand : ILoginCommand
    {
        private readonly ITokensEngine tokensEngine;
        private readonly IRequestClient<InternalLoginRequest> client;
        private ILogger<LoginCommand> logger;

        /// <summary>
        /// Get user ID from UserService.
        /// </summary>
        private async Task<UserCredential> GetUserCredential(InternalLoginRequest request)
        {
            var response = await client.GetResponse<OperationResult<UserCredential>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        private bool CheckUserCredentials(UserCredential credential, LoginRequest request)
        {
            string passwordHash = ShaHash.GetPasswordHash(request.Password);

            return credential.PasswordHash == passwordHash
                && credential.Email == request.Email;
        }

        public LoginCommand(
            [FromServices] ITokensEngine tokensEngine,
            [FromServices] IRequestClient<InternalLoginRequest> client,
            [FromServices] ILogger<LoginCommand> logger)
        {
            this.tokensEngine = tokensEngine;
            this.client = client;
            this.logger = logger;
        }

        public async Task<UserToken> Execute(LoginRequest request)
        {
            var internalRequest = new InternalLoginRequest()
            {
                Email = request.Email,
                Password = request.Password
            };

            var credential = new UserCredential();

            try
            {
                credential = await GetUserCredential(internalRequest);
            }
            catch (NotFoundException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);

                var ex = new NotFoundException(errorData.Item3);
                logger.LogWarning(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }

            if (!CheckUserCredentials(credential, request))
            {
                var error = $"User {request.Email} entered wrong password.";
                var e = new ForbiddenException(error);
                logger.LogWarning(e, $"{Guid.NewGuid()}_{error}");
                throw e;
            }

            return tokensEngine.GetToken(credential.UserId);
        }
    }
}
