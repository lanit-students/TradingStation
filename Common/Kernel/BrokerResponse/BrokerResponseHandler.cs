using Automatonymous;
using DTO;
using Kernel.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Kernel
{
    public static class BrokerResponseHandler
    {
        public static T HandleResponse<T>(BrokerResponse<T> response)
        {
            var statusCode = response.StatusCode;
            var message = response.Message;

            if (statusCode != 200)
            {
                switch (statusCode)
                {
                    case 400:
                        throw new BadRequestException(message);
                    case 403:
                        throw new ForbiddenException(message);
                    case 404:
                        throw new NotFoundException(message);
                    default:
                        throw new InternalServerException(message);
                }
            }

            return response.Response;
        }
    }
}
