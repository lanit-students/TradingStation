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
            command.Execute(user);
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public void DeleteUser([FromServices] ICommand<Guid> command, [FromBody] Guid id)
        {
            command.Execute(id);
        }
    }
}
