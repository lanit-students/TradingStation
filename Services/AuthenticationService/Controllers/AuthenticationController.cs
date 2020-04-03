using Microsoft.AspNetCore.Mvc;

using AuthenticationService.Interfaces;

using DTO;
using FluentValidation;

using System.Text.Json;

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
        public string Login([FromServices] ILoginCommand command, [FromBody] UserEmailPassword user)
        {
            var result = command.Execute(user);

            var userToken = result.Result;

            return JsonSerializer.Serialize(userToken);
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
        public void Logout([FromServices] ICommand<int> command, [FromQuery] int userId)
        {
            command.Execute(userId);
        }
    }
}
