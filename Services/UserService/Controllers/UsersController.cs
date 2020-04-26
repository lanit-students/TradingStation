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
using System.Net.Mail;
using System.Net;

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
            var result = await command.Execute(request);

            if (result)
            {
                var email = request.Email;
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress("traidplatform@mail.ru", "Trading Station");
                // кому отправляем
                MailAddress to = new MailAddress(request.Email);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Registration confirmation";
                // текст письма
                m.Body = $"Please, click this link https://localhost:5011/users/confirm?secretToken={email}";
                // письмо представляет код html
                m.IsBodyHtml = false;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("traidplatform@mail.ru", "t123plat");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            return true;
        }

        [Route("confirm")]
        [HttpGet]
        public async Task<bool> ConfirmUser([FromServices] IConfirmUserCommand command, [FromQuery] string secretToken)
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
