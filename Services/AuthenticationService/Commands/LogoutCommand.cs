using AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthenticationService.Commands
{
    public class LogoutCommand : ICommand<Guid>
    {
        private readonly ITokensEngine tokensEngine;

        public LogoutCommand([FromServices] ITokensEngine tokensEngine)
        {
            this.tokensEngine = tokensEngine;
        }

        public void Execute(Guid data)
        {
            tokensEngine.DeleteToken(data);
        }
    }
}
