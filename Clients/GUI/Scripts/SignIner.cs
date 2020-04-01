using DTO;
using HttpWebRequestWrapperLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class SignIner
    {
        public static bool SignIn(UserEmailPassword userInput)
        {
            var requestWrapper = new HttpWebRequestWrapper();
            try
            {
                var result = requestWrapper.Post("http://localhost:5001/authentication/login", null, userInput);
                
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
