using System;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Http;
using UserService.Properties;

namespace UserService.Utils
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;

        public TokenMiddleware(RequestDelegate next)
        {
            this.next = next;
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
                //ans = restClient.Post(Urls.AuthServiceCheckToken, null, userToken);
            }
            // TODO add logs
            catch (Exception)
            {
                ans = null;
            }
            //return string.Compare(ans, Messages.TokenError, StringComparison.OrdinalIgnoreCase) == 0 ;

            return true;
        }
    }
}
