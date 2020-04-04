using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DTO;
using AuthenticationService.Interfaces;
using DTO.RestRequests;

using System.Text.Json;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
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
        /// Checks if token exist.
        /// </summary>
        [Route("check")]
        [HttpPost]
        public bool CheckToken([FromServices] IValidator<UserToken> validator, [FromBody] UserToken token)
        {
            validator.ValidateAndThrow(token);

            return tokenEngine.CheckToken(token);
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
