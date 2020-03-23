using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserService.Commands;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        
        [Route("create")]
        [HttpPost] 
        /// <summary>
        /// This method will be implemented in communication with other services
        /// </summary>
        public HttpStatusCode CreateUser([FromServices] ICreateUserCommand command, [FromBody] UserCredential userCredential)
        {
            return command.Execute(userCredential);
        }
    }
}
