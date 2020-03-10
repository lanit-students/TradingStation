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
        public string GetToken()
            => TokensStorage.Get();

        /// <summary>
        /// Checks if token is active
        /// </summary>
        [Route("[controller]/check")]
        [HttpGet]
        public bool CheckToken(string token)
            => TokensStorage.Check(token);

        [Route("[controller]/delete")]
        [HttpGet]
        public void DeleteToken(string token)
            => TokensStorage.Delete(token);
    }
}
