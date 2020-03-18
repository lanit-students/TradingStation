using Microsoft.AspNetCore.Mvc;

using AuthenticationService.Interfaces;

using DTO;
using Kernel;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokensEngine tokenEngine;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AuthenticationController([FromServices] ITokensEngine tokenEngine)
        {
            this.tokenEngine = tokenEngine;
        }

        /// <summary>
        /// Login user in system.
        /// Generates and returns an active token.
        /// </summary>
        [Route("login")]
        [HttpPost]
        public string Login([FromServices] ICommand<User, string> command, [FromBody] User user)
        {
            return command.Execute(user);
        }

        /// <summary>
        /// Checks if token exist.
        /// </summary>
        [Route("check")]
        [HttpGet]
        public bool CheckToken([FromQuery] int userId, [FromQuery] string token)
        {
            CommonValidations.ValidateId(userId);

            return tokenEngine.CheckToken(userId, token);
        }

        /// <summary>
        /// Deletes a token.
        /// </summary>
        [Route("logout")]
        [HttpDelete]
        public void Logout([FromServices] ICommand<int> command, [FromQuery] int userId)
        {
            command.Execute(userId);
        }
    }
}
