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
        public async Task<bool> DeleteUser([FromServices] IDeleteUserCommand command, [FromBody] UserIdRequest request)
        {
            return await command.Execute(request);
        }

        [Route("get")]
        [HttpGet]
        public async Task<User> GetUser([FromServices] IGetUserByIdCommand command, [FromQuery] Guid userId)
        {
            return await command.Execute(userId);
        }
    }
}
