using AuthenticationService.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthenticationService.Commands
{
    public class LoginCommand : ICommand<User, string>
    {
        private readonly ITokensEngine tokensEngine;

        /// <summary>
        /// Get user ID from UserService.
        /// </summary>
        private Guid GetUserIdFromUserService(User user)
        {
            return Guid.Empty;
        }

        public LoginCommand([FromServices] ITokensEngine tokensEngine)
        {
            this.tokensEngine = tokensEngine;
        }

        public string Execute(User data)
        {
            Guid userId = GetUserIdFromUserService(data);

            return tokensEngine.GetToken(userId);
        }
    }
}
