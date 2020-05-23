using DTO.RestRequests;
using IDeleteUserUserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;
using Microsoft.Extensions.Logging;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;

        public UsersController([FromServices] ILogger<UsersController> logger)
        {
            this.logger = logger;
        }

        [Route("create")]
        [HttpPost]
        public async Task<bool> CreateUser([FromServices] ICreateUserCommand command, [FromBody] CreateUserRequest request)
        {
            logger.LogInformation("Create user request received from GUI to UserService");
            return await command.Execute(request);
         }

        [Route("confirm")]
        [HttpGet]
        public async Task<bool> ConfirmUser([FromServices] IConfirmUserCommand command, [FromQuery] Guid secretToken)
        {
            logger.LogInformation("Confirm user request received from Email to UserService");
            return await command.Execute(secretToken);
        }

        [Route("edit")]
        [HttpPut]
        public async Task<bool> EditUser([FromServices] IEditUserCommand command, [FromBody] EditUserRequest request)
        {
            logger.LogInformation("Edit user request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<bool> DeleteUser([FromServices] IDeleteUserCommand command, [FromBody] DeleteUserRequest request)
        {
            logger.LogInformation("Delete user request received from GUI to UserService");
            return await command.Execute(request);
        }

        [Route("get")]
        [HttpGet]
        public async Task<UserInfoRequest> GetUser([FromServices] IGetUserByIdCommand command, [FromHeader] Guid userId)
        {
            logger.LogInformation("Get user request received from GUI to UserService");
            return await command.Execute(userId);
        }
    }
}
