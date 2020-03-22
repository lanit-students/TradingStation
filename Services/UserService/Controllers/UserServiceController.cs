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
        public HttpStatusCode DeleteUserFromDataBaseService([FromServices] IDeleteUserCommand command, [FromQuery] Guid userId)
        {
            return command.Execute(userId);
        }
    }
}
