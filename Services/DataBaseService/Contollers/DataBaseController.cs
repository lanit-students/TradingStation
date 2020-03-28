using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DataBaseService.DbModels;
using DataBaseService.Interfaces;

namespace DataBaseService.Contollers
{
    [Route("[controller]")]
    [ApiController]
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
