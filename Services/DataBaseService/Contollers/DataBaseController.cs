using Microsoft.AspNetCore.Mvc;
using DTO;
using DataBaseService.Interfaces;

namespace DataBaseService.Contollers
{
    [ApiController]
    [Route("[controller]")]    
    public class DataBaseController : ControllerBase
    {
        [Route("CreateUser")]
        [HttpPost]
        public void CreateUser([FromBody] UserEmailPassword userIn, [FromServices] ICommand<UserEmailPassword> command)
        {            
            command.Execute(userIn);            
        }
    }
}
