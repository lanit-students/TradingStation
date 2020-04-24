using DTO.RestRequests;
using IDeleteUserUserService.Interfaces;
using Kernel.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;
using UserService.Utils;
using Microsoft.Extensions.Logging;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<UserConfirmation> userManager;
        public UsersController(UserManager<UserConfirmation> userManager)
        {
            this.userManager = userManager;
        }
        private readonly ILogger<UsersController> logger;

        public UsersController([FromServices] ILogger<UsersController> logger)
        {
            this.logger = logger;
        }

        [Route("create")]
        [HttpPost]
        public async Task<bool> CreateUser([FromServices] ICreateUserCommand command, [FromBody] CreateUserRequest request)
        {
            var result = await command.Execute(request);

            var userConfirm = new UserConfirmation
            {
                Id = result.Id.ToString(),
                Email = result.Email
            };

            // генераци€ токена дл€ пользовател€
            var code = await userManager.GenerateEmailConfirmationTokenAsync(userConfirm);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = userConfirm.Id, code = code },
                protocol: HttpContext.Request.Scheme);
            var emailSender = new EmailSender();
            await emailSender.SendEmailAsync(userConfirm.Email, "Confirm your account",
                $"ѕодтвердите регистрацию, перейд€ по ссылке: <a href='{callbackUrl}'>link</a>");

            return true;
            //return Content("ƒл€ завершени€ регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
            logger.LogInformation("Create user request received from GUI to UserService");
            return await command.Execute(request);
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
