using System;
using Microsoft.AspNetCore.Mvc;
using IDeleteUserUserService.Interfaces;
using DTO;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        [Route("create")]
        [HttpPost] 
        /// <summary>
        /// This method will be implemented in communication with other services
        /// </summary>
        public void CreateUser([FromServices] ICreateUserCommand command, [FromBody] UserEmailPassword userEmailPassword)
        {
            command.Execute(userEmailPassword);
        }

        [Route("delete")]
        [HttpDelete]
        public void DeleteUser([FromServices] IDeleteUserCommand command, [FromQuery] Guid userId)
        {
             command.Execute(userId);
        }
    }
}
