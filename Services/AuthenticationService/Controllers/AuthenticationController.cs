using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Generates and returns an active token
        /// </summary>
        [Route("[controller]/get")]
        [HttpGet]
        public string Login(int id)
            => TokensStorage.GetToken(id);

        /// <summary>
        /// Checks if token is active
        /// </summary>
        [Route("[controller]/check")]
        [HttpGet]
        public bool CheckToken(int id,string token)
            => TokensStorage.CheckToken(id,token);

        /// <summary>
        /// Deletes a token
        /// </summary>
        [Route("[controller]/delete")]
        [HttpGet]
        public void Logout(int id,string token)
            => TokensStorage.DeleteToken(id,token);
    }
}
