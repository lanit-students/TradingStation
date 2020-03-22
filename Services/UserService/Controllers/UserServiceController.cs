using System;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        [Route("[controller]/deleteUser")]
        [HttpGet]
        public string DeleteUserFromDataBaseService([FromQuery] Guid userId)
        {
            return "Hey";
            //IDeleteUser<Guid, string> deletecommand =new DeleteUserCommand();
            //return deletecommand.Execute(userId);
        }
    }
}