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

        /// <summary>
        /// Constructor for lulz and some data to play with
        /// </summary>
       /* public AuthentificationController()
        {
            tokens = new List<string>();
        }*/

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
            var guidString = guid.ToString();
            var index = BinarySearch(guidString);
            int compare = String.Compare(TokensStorage.Tokens[index],guidString);

            if (compare < 0)
            {
                index++;
            }

            TokensStorage.Tokens.Insert(index, guid.ToString());
            return guid.ToString();
        }
            

        /// <summary>
        /// Checks if token is active
        /// </summary>
        [Route("[controller]/check")]
        [HttpGet]
        public string CheckToken(string token)
        {
            var index = BinarySearch(token);
            int compare = String.Compare(TokensStorage.Tokens[index], token);
            if (compare != 0)
            {
                return "false";
            }
            return "true";
        }


        [Route("[controller]/delete")]
        [HttpGet]
        public void DeleteToken(string token)
        {
            int index = BinarySearch(token);
            TokensStorage.Tokens.RemoveAt(index);
        }
        private int BinarySearch(string token)
        {
            int left = 0;
            int right = TokensStorage.Tokens.Count - 1;
            int middle;
            int compare;
            while (left < right)
            {
                middle = (left + right) / 2;

                compare = String.Compare(TokensStorage.Tokens[middle], token);
                if (compare < 0)
                {
                    left = middle + 1;
                }
                if (compare > 0)
                {
                    right = middle - 1;
                }
                if (compare == 0)
                {
                    return middle;
                }
            }
            return left;
        }
    }
}
