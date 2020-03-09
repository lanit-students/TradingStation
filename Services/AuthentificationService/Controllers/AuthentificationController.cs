using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AuthentificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthentificationController : ControllerBase
    {
        /// <summary>
        /// Storage for tokens 
        /// May use it before BD is created
        /// </summary>
        private List<string> tokens;

        /// <summary>
        /// Temporary constructortor logic
        /// to see some outupt
        /// </summary>
        public AuthentificationController()
        {
            tokens = new List<string>();

            for (var i = 0; i < 100; ++i)
            {
                tokens.Add("token" + i.ToString());
            }
        }

        /// <summary>
        /// Returns list of existing tokens (just an example)
        /// </summary>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enumerable.Range(0, tokens.Count - 1).Select(index => tokens[index])
            .ToArray();
        }
    }
}
