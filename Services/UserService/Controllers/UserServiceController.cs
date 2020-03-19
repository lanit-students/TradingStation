using Microsoft.AspNetCore.Mvc;
using DTO;
using UserService.Interfaces;
using UserService.Commands;

namespace UserService.Controllers
{
    [ApiController]
    public class UserServiceController : Controller
    {
        
        [Route("[controller]/post")]
        [HttpPost] 
        /// <summary>
        /// This method will be implemented in communication with other services
        /// </summary>
        public string CreateUser([FromQuery] string email, string password)
        {
            ICreateUser<string, string> command = new CreateUserCommand();
            return command.Execute(email, password);
        }
    }
}
