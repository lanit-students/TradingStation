using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MassTransit;

using AuthenticationService.Interfaces;
using DTO;
using Kernel.CustomExceptions;
using Kernel;
using DTO.RestRequests;

namespace AuthenticationService.Commands
{
    public class LoginCommand : ILoginCommand
    {
        private readonly ITokensEngine tokensEngine;
        private readonly IBus busControl;

        /// <summary>
        /// Get user ID from UserService.
        /// </summary>
        private async Task<UserCredential> GetUserCredential(LoginRequest request)
        {
            var uri = new Uri("rabbitmq://localhost/UserService");

            var client = busControl.CreateRequestClient<LoginRequest>(uri).Create(request);

            var response = await client.GetResponse<UserCredential>();

            return response.Message;
        }

        private bool CheckUserCredentials(UserCredential credential, LoginRequest request)
        {
            string passwordHash = ShaHash.GetPasswordHash(request.Password);

            return credential.PasswordHash == passwordHash
                && credential.Email == request.Email;
        }

        public LoginCommand(
            [FromServices] ITokensEngine tokensEngine,
            [FromServices] IBus busControl)
        {
            this.tokensEngine = tokensEngine;
            this.busControl = busControl;
        }

        public async Task<UserToken> Execute(LoginRequest request)
        {
            UserCredential credential = await GetUserCredential(request);

            if (!CheckUserCredentials(credential, request))
            {
                throw new ForbiddenException();
            }

            return tokensEngine.GetToken(credential.UserId);
        }
    }
}
