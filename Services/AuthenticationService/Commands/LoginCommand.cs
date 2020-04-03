using AuthenticationService.Interfaces;
using System;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Commands
{
    public class LoginCommand : ICommand<UserEmailPassword, string>
    {
        private readonly ITokensEngine tokensEngine;

        /// <summary>
        /// Get user ID from UserService.
        /// </summary>
        private Guid GetUserIdFromUserService(UserEmailPassword user)
        {
            return Guid.Empty;
        }

        public LoginCommand([FromServices] ITokensEngine tokensEngine)
        {
            this.tokensEngine = tokensEngine;
        }

        public string Execute(UserEmailPassword data)
        {
            Guid userId = GetUserIdFromUserService(data);

            return tokensEngine.GetToken(userId);
        }
    }
}
