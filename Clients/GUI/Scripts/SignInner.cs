﻿using DTO;
using Kernel;
using Kernel.Enums;
using System;

namespace GUI.Scripts
{
    public static class SignInner
    {
        public static void SignIn(UserCredential userInput)
        {
            const string url = "http://localhost:5001/authentication/login";

            var token = new RestClient<UserCredential, UserToken>(url, RestRequestType.POST).Execute(userInput);
        }
    }
}
