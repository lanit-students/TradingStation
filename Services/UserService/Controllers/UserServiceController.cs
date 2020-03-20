using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IDeleteUserUserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using UserService.Commands;

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