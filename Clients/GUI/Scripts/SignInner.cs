using DTO;
using Kernel;
using Kernel.Enums;
using System;

namespace GUI.Scripts
{
    public static class SignInner
    {
        public static void SignIn(UserEmailPassword userInput)
        {
            const string url = "http://localhost:5001/authentication/login";

            var token = new RestClient<UserEmailPassword, UserToken>(url, RestRequestType.POST).Execute(userInput);
        }
    }
}
