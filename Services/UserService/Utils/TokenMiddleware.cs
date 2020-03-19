﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserService.Properties;

namespace UserService.Utils
{
    public class TokenMiddleware
    {
        private RequestDelegate next;

        public TokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "POST" & string.Compare(context.Request.Path, StringCons.UrlCreateUser, StringComparison.OrdinalIgnoreCase) == 0)
            {
                await next.Invoke(context);
                return;
            }

            string token = context.Request.Query["token"];
            string userId = context.Request.Query["userId"];

            if (checkToken(userId, token))
            {
                await next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(StringCons.TokenError);
            }
        }

        private bool checkToken(string userId, string token)
        {
            string ans;
            using (var client = new HttpClient())
            {
                try
                {
                    ans = client
                        .GetStringAsync($"https://localhost:5001/Authentication/check?userId={userId}&token={token}")
                        .Result;
                }
                catch (Exception)
                {
                    ans = null;
                }
            }
            return string.Compare(ans, StringCons.UrlCreateUser, StringComparison.OrdinalIgnoreCase) == 0 ;
        }
    }
}