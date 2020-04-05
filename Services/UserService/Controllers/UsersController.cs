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
    public class UsersController : ControllerBase
    {
        [Route("create")]
        [HttpPost]
        public async Task<bool> CreateUser([FromServices] ICreateUserCommand command, [FromBody] CreateUserRequest request)
        {
            return await command.Execute(request);
        }

        [Route("edit")]
        [HttpPut]
        public async Task<bool> EditUser([FromServices] IEditUserCommand command, [FromBody] EditUserRequest request)
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
