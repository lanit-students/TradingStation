using Microsoft.AspNetCore.Mvc;
using DTO;
using UserService.Interfaces;


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
        public string CreateUser([FromServices] string email, string password)
        {
            ICreateUser<string, string> command = new CreateUserCommand();
            return command.Execute(email, password);
        }

        // This one is to test functionality of project 
        // Add in the url - path "/UserService/get/email=will&password=work"
        [Route("[controller]/get")]
        [HttpGet]
        public string TestCreateUserLogic([FromQuery] string email, string password)
        {
            ICreateUser<string, string> command = new CreateUserCommand();
            return command.Execute(email, password);
        }
    }
}
