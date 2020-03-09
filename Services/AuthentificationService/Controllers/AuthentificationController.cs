using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AuthentificationService.Controllers
{
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        /// <summary>
        /// Storage for tokens 
        /// May be used until database is created
        /// </summary>
        private List<string> tokens;

        /// <summary>
        /// Constructor for lulz and some data to play with
        /// </summary>
        public AuthentificationController()
        {
            tokens = new List<string>() { "secret", "supersecret", "megasecret" };
        }

        /// <summary>
        /// Generates and returns an active token
        /// </summary>
        [Route("[controller]/get")]
        [HttpGet]
        public string GetToken()
            => tokens[new Random().Next(0, tokens.Count())];

        /// <summary>
        /// Checks if token is active
        /// </summary>
        [Route("[controller]/check")]
        [HttpGet]
        public string CheckToken(string token)
            => tokens.Contains(token).ToString();
    }
}
