using System;

using Microsoft.AspNetCore.Mvc;

using AuthenticationService.Interfaces;
using Kernel.CustomExceptions;

namespace AuthenticationService.Commands
{
    public class LogoutCommand : ILogoutCommand
    {
        private readonly ITokensEngine _tokensEngine;

        public LogoutCommand([FromServices] ITokensEngine tokensEngine)
        {
            _tokensEngine = tokensEngine;
        }

        public bool Execute(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new BadRequestException();

            return _tokensEngine.DeleteToken(userId).IsSuccess;
        }
    }
}
