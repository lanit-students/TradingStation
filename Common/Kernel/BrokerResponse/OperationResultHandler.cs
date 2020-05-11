using DTO;
using Kernel.CustomExceptions;

namespace Kernel
{
    public static class OperationResultHandler
    {
        public static T HandleResponse<T>(OperationResult<T> response)
        {
            var statusCode = response.StatusCode;

            if (statusCode != 200)
            {
                switch (statusCode)
                {
                    case 400:
                        throw new BadRequestException(response.ErrorMessage);
                    case 403:
                        throw new ForbiddenException(response.ErrorMessage);
                    case 404:
                        throw new NotFoundException(response.ErrorMessage);
                    default:
                        throw new InternalServerException(response.ErrorMessage);
                }
            }

            return response.Data;
        }
    }
}
