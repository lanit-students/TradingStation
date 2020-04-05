using AuthenticationService.Utils;
using DTO;
using DTO.RestRequests;
using IDeleteUserUserService.Interfaces;
using Kernel.CustomExceptions;
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
        /// <summary>
        /// This method will be implemented in communication with other services
        /// </summary>
        [Route("create")]
        [HttpPost]
        public async Task<bool> CreateUser([FromServices] ICreateUserCommand command, [FromBody] CreateUserRequest request)
        {
            CreateUserRequesValidator validator = new CreateUserRequesValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                string errMessage = "";
                foreach (var i in validationResult.Errors)
                    errMessage += i.ErrorMessage +"  \n";
                throw new BadRequestException(errMessage);
            }

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
