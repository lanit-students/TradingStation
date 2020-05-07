using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MassTransit;

using DTO;
using Kernel.CustomExceptions;
using Kernel.Enums;

namespace Kernel.Middlewares
{
    public class CheckTokenMiddleware
    {
        private readonly IBus _bus;
        private readonly RequestDelegate _next;

        public CheckTokenMiddleware(RequestDelegate next, [FromServices] IBus bus)
        {
            _next = next;
            _bus = bus;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if ((string.Equals(context.Request.Method, RestRequestType.POST.ToString(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(context.Request.Path, "/users/create", StringComparison.OrdinalIgnoreCase))
                ||(string.Equals(context.Request.Method, RestRequestType.GET.ToString(), StringComparison.OrdinalIgnoreCase)
                && string.Equals(context.Request.Path, "/users/confirm", StringComparison.OrdinalIgnoreCase)))
            {
                await _next.Invoke(context);

                return;
            }

            string tokenBody = context.Request.Headers["token"];
            Guid.TryParse(context.Request.Headers["userId"], out Guid userId);
            if (userId == Guid.Empty)
                throw new ForbiddenException();

            var userToken = new UserToken
            {
                UserId = userId,
                Body = tokenBody
            };

            bool isTokenCorrect = await CheckToken(userToken);

            if (isTokenCorrect)
            {
                await _next.Invoke(context);
            }
            else
            {
                throw new ForbiddenException();
            }
        }

        private async Task<bool> CheckToken(UserToken token)
        {
            var uri = new Uri("rabbitmq://localhost/AuthService");

            var client = _bus.CreateRequestClient<UserToken>(uri).Create(token);

            var response = await client.GetResponse<OperationResult>();

            return response.Message.IsSuccess;
        }
    }
}
