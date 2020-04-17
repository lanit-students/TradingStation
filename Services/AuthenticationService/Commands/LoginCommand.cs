using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MassTransit;

using AuthenticationService.Interfaces;
using DTO;
using Kernel.CustomExceptions;
using Kernel;
using DTO.RestRequests;
using DTO.BrokerRequests;

namespace AuthenticationService.Commands
{
    public class LoginCommand : ILoginCommand
    {
        private readonly ITokensEngine tokensEngine;
        private readonly IRequestClient<InternalLoginRequest> client;

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
            [FromServices] IRequestClient<InternalLoginRequest> client)
        {
            this.tokensEngine = tokensEngine;
            this.client = client;
        }

        public async Task<UserToken> Execute(LoginRequest request)
        {
            var internalRequest = new InternalLoginRequest()
            {
                Email = request.Email,
                Password = request.Password
            };

            var credential = await GetUserCredential(internalRequest);

            if (!CheckUserCredentials(credential, request))
            {
                throw new ForbiddenException();
            }

            return tokensEngine.GetToken(credential.UserId);
        }
    }
}
