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
        /// May use it untils BD is created
        /// </summary>
        private List<string> tokens;

        /// <summary>
        /// Constructor for lulz
        /// </summary>
        public AuthentificationController()
        {
            tokens = new List<string>() { "secret", "supersecret", "megasecret" };
        }

        /// <summary>
        /// Generates and return an active token
        /// </summary>
        [Route("[controller]/get")]
        [HttpGet]
        public string Get()
            => tokens[new Random().Next(0, tokens.Count())];

        /// <summary>
        /// Checks if token is active
        /// </summary>
        [Route("[controller]/check")]
        [HttpGet]
        public string Get(string token)
            => tokens.Contains(token).ToString();
    }
}
