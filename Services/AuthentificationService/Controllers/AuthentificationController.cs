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
        {
            var date = DateTime.Now;
            var randomDigit = new Random();
            var valueForToken1 = randomDigit.Next(-400000000, 400000000);
            var valueForToken2 = randomDigit.Next(-60000, 60000);
            var valueForToken3 = randomDigit.Next(-60000, 60000);
            var valueForToken4 = randomDigit.Next(-200, 200);
            var byteArrayForGuid = new byte[]
            {
                (byte)date.Day,
                (byte)date.Month,
                (byte)(date.Year % 100),
                (byte)(date.Year / 100),
                (byte)date.Second,
                (byte)date.Minute,
                (byte)date.Hour,
                (byte)valueForToken4,
            };
            Guid guid = new Guid
                (
                valueForToken1,
                (short)valueForToken2,
                (short)valueForToken3,
                byteArrayForGuid
                );

            var index = BinarySearch(guid.ToString());
            tokens.Insert(index, guid.ToString());
            return guid.ToString();
        }
            

        /// <summary>
        /// Checks if token is active
        /// </summary>
        [Route("[controller]/check")]
        [HttpGet]
        public string CheckToken(string token)
            => tokens.Contains(token).ToString();
    }
}
