using AuthenticationService.Interfaces;
using Kernel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
