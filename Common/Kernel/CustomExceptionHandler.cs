using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using Kernel.CustomExceptions;

namespace Kernel
{
    public static class CustomExceptionHandler
    {
        public static async Task HandleCustomException(HttpContext context)
        {
            var handler = context.Features.Get<IExceptionHandlerPathFeature>();

            var errorResponse = new ErrorResponse
            {
                UtcTime = DateTime.UtcNow
            };

            if (handler?.Error is BaseException)
            {
                var exception = (BaseException)handler?.Error;

                context.Response.StatusCode = exception.StatusCode;

                errorResponse.Header = exception.Header;
                errorResponse.Message = ErrorMessageFormatter.GetMessageData(exception.Message).Item3;
            }
            else
            {
                context.Response.StatusCode = 500;

                errorResponse.Message = "Internal server error";
            }

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
