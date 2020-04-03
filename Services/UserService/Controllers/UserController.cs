using DTO;
using IDeleteUserUserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public Guid CreateUser([FromServices] ICreateUserCommand command, [FromBody] UserEmailPassword userEmailPassword)
        {
            var res = command.Execute(userEmailPassword);

            return res.Result;
        }

        [Route("delete")]
        [HttpDelete]
        public void DeleteUser([FromServices] IDeleteUserCommand command, [FromQuery] Guid userId)
        {
             command.Execute(userId);
        }
    }
}
