using System;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        [Route("[controller]/deleteUser")]
        [HttpGet]
        public HttpStatusCode DeleteUserFromDataBaseService([FromServices] IDeleteUser command,[FromQuery] int userId)
        {
            return command.Execute(userId);
        }
    }
}