using AuthenticationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Kernel;

namespace AuthenticationService.Commands
{
    public class LoginCommand : ICommand<User, string>
    {
        private readonly ITokensEngine tokensEngine;

        /// <summary>
        /// Get user ID from UserService.
        /// </summary>
        private int GetUserIdFromUserService(User user)
        {
            return -1;
        }

        public LoginCommand([FromServices] ITokensEngine tokensEngine)
        {
            this.tokensEngine = tokensEngine;
        }

        public string Execute(User data)
        {
            int userId = GetUserIdFromUserService(data);

            CommonValidations.ValidateId(userId);

            return tokensEngine.GetToken(userId);
        }
    }
}
