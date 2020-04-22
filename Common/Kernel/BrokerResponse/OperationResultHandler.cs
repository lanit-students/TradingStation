using DTO;
using Kernel.CustomExceptions;

namespace Kernel
{
    public static class OperationResultHandler
    {
        public static T HandleResponse<T>(OperationResult<T> response)
        {
            var statusCode = response.StatusCode;
            var message = response.ErrorMessage;

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

            return response.Data;
        }
    }
}
