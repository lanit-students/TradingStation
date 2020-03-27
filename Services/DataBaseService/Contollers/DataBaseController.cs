using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DataBaseService.DbModels;

namespace DataBaseService.Contollers
{
    [Route("[controller]")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        [Route("CreateUser")]
        [HttpPost]
        public void CreateUser([FromBody] UserEmailPassword userIn, [FromServices] DataBaseContext db)
        {
            var newUser = new DbUser { Id = Guid.NewGuid(), Email = userIn.Email, Password = userIn.Password };

            db.Users.Add(newUser);
            db.SaveChanges();
        }
    }
}
