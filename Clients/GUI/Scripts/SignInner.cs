﻿using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class SignInner
    {
        public static async Task<UserToken> SignIn(LoginRequest request)
        {
            const string url = "https://localhost:5001/authentication/login";

            var client = new RestClient<LoginRequest, UserToken>(url, RestRequestType.POST);

            var userToken = await client.Execute(request);

            return userToken;
        }
    }
}
