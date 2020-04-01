using DTO;
using HttpWebRequestWrapperLib;
using System;

namespace GUI.Scripts
{
    public class SignInner
    {
#pragma warning disable CS8632
        public static string? SignIn(UserEmailPassword userInput)
        {
            var requestWrapper = new HttpWebRequestWrapper();
            try
            {
                var result = requestWrapper.Post("http://localhost:5001/authentication/login", null, userInput);
                
            }
            catch(Exception e)
            {
                return e.Message;
            }
            return null;
        }
    }
}
