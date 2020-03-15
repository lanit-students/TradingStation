using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            if (context.Request.Method == "POST")
            {
                await next.Invoke(context);
                return;
            }
            Console.WriteLine("1");

            string token = context.Request.Query["token"];
            string userId = context.Request.Query["userId"];

            if (checkToken(userId, token))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
            {
                await next.Invoke(context);
            }
        }

        private bool checkToken(string userId, string token)
        {
            string ans;
            using (var client = new HttpClient())
            {
                ans = client.GetStringAsync($"https://localhost:5001/Authentication/check?userId={userId}&token={token}").Result;
            }

            return ans == "true";
        }


    }
}
