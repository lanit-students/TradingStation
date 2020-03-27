using System;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;
using HttpWebRequestWrapperLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Properties;

namespace UserService.Utils
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;
        private readonly HttpWebRequestWrapper httpWebWrapper;

        public TokenMiddleware(RequestDelegate next, [FromServices] HttpWebRequestWrapper httpWebWrapper)
        {
            this.next = next;
            this.httpWebWrapper = httpWebWrapper;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "POST" 
                && context.Request.Path == Urls.CreateUser)
            {
                await next.Invoke(context);
                return;
            }

            string token = context.Request.Headers["token"];
            Guid userId;
            try
            {
                userId = Guid.Parse(context.Request.Headers["userId"]);
            }
            catch
            {
                //TODO change on custom exception
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(Messages.IdError);
                return;
            }


            if (CheckToken(userId, token))
            {
                await next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(Messages.TokenError);
            }
        }

        private bool CheckToken(Guid userId, string token)
        {
            string ans;
            try
            {
                //TODO replace way of communication
                var userToken = new UserToken {UserId = userId, Body = token};
                ans = httpWebWrapper.Post(Urls.AuthServiceCheckToken, null, userToken);
            }
            // TODO add logs
            catch (Exception) 
            {   
                ans = null;
            }
            return string.Compare(ans, Messages.TokenError, StringComparison.OrdinalIgnoreCase) == 0 ;
        }
    }
}
