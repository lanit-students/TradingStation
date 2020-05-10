using System;

using Microsoft.AspNetCore.Mvc;

using AuthenticationService.Interfaces;
using Kernel.CustomExceptions;
using Microsoft.Extensions.Logging;

namespace AuthenticationService.Commands
{
    public class LogoutCommand : ILogoutCommand
    {
        private ILogger<LogoutCommand> logger;

        public LogoutCommand([FromServices] ILogger<LogoutCommand> logger)
        {
            this.logger = logger;
        }

        public bool Execute(Guid userId)
        {
            logger.LogInformation($"User {userId} trying to log out.");

            if (userId == Guid.Empty)
            {
                var e = new BadRequestException("Empty id in log out request.");
                logger.LogWarning(e, "Attempt to logout user with empty id.");
                throw new BadRequestException();
            }

            logger.LogInformation($"User {userId} logged out.");

            return true;
        }
    }
}
