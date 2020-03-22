using System;
using Microsoft.AspNetCore.Mvc;

using IDeleteUserUserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserServiceController : ControllerBase
    {
        [Route("delete")]
        [HttpDelete]
        public void DeleteUser([FromServices] IDeleteUserCommand command, [FromQuery] Guid userId)
        {
             command.Execute(userId);
        }
    }
}
