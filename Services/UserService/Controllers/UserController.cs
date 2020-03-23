using System;
using Microsoft.AspNetCore.Mvc;

using IDeleteUserUserService.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserService.Commands;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
<<<<<<< HEAD
    public class UserController : ControllerBase
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

        [Route("delete")]
        [HttpDelete]
        public void DeleteUser([FromServices] IDeleteUserCommand command, [FromQuery] Guid userId)
        {
             command.Execute(userId);
        }
    }
}
