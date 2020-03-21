using Microsoft.AspNetCore.Mvc;
using UserService.Commands;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserServiceController : Controller
    {
        
        [Route("createUser")]
        [HttpPost] 
        /// <summary>
        /// This method will be implemented in communication with other services
        /// </summary>
        public string CreateUser([FromServices] CreateUserCommand command, [FromBody] string email, [FromBody] string password)
        {
            return command.Execute(email, password);
        }
    }
}
