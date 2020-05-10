using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DTO;
using DTO.RestRequests;
using AuthenticationService.Interfaces;
using Microsoft.Extensions.Logging;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController([FromServices] ILogger<AuthenticationController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Login user in system.
        /// Generates and returns an active token.
        /// </summary>
        [Route("login")]
        [HttpPost]
        public async Task<UserToken> Login([FromServices] ILoginCommand command, [FromBody] LoginRequest request)
        {
            return await command.Execute(request);
        }

        /// <summary>
        /// Deletes a token.
        /// </summary>
        [Route("logout")]
        [HttpDelete]
        public bool Logout([FromServices] ILogoutCommand command, [FromHeader] Guid userId)
        {
            return command.Execute(userId);
        }
    }
}
