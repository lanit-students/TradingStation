using Microsoft.AspNetCore.Mvc;
using DTO;
using DataBaseService.Interfaces;
using System;

namespace DataBaseService.Contollers
{
    [ApiController]
    [Route("[controller]")]    
    public class DataBaseController : ControllerBase
    {
        [Route("CreateUser")]
        [HttpPost]
        public void CreateUser([FromServices] ICommand<UserEmailPassword> command, [FromBody] UserEmailPassword user)
        {
            if (user.PasswordHash == null || user.Email == null)
            {
                throw new Exception("Not correct data");
            }
            command.Execute(user);

        }
    }
}
