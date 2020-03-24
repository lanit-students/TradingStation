using AuthenticationService.Interfaces;
using System;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Commands
{
    public class LoginCommand : ICommand<User, UserToken>
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

        public UserToken Execute(User data)
        {
            Guid userId = GetUserIdFromUserService(data);

            return tokensEngine.GetToken(userId);
        }
    }
}
