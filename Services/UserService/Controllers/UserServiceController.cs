using System;
using System.Net;
using IDeleteUserUserService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        [Route("[controller]/DeleteUser")]
        [HttpGet]
        public void DeleteUserFromDataBaseService([FromServices] IDeleteUserCommand command, [FromQuery] Guid userId)
        {
             command.Execute(userId);
        }
    }
}
