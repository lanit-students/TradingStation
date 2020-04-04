using DTO;
using DTO.RestRequests;
using IDeleteUserUserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// This method will be implemented in communication with other services
        /// </summary>
        [Route("create")]
        [HttpPost]
        public async Task<bool> CreateUser([FromServices] ICreateUserCommand command, [FromBody] CreateUserRequest request)
        {
            return await command.Execute(request);
        }

        [Route("delete")]
        [HttpDelete]
        public bool DeleteUser([FromServices] IDeleteUserCommand command, [FromHeader] Guid userId)
        {
            return command.Execute(userId);
        }

        [Route("get")]
        [HttpGet]
        public User GetUser([FromHeader] Guid userId)
        {
            return null;
        }
    }
}
