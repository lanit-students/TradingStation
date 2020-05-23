using DTO;
using Kernel.CustomExceptions;
using System;

namespace Kernel
{
    public static class OperationResultWrapper
    {
        public static OperationResult<TRes> CreateResponse<T, TRes>(Func<T, TRes> func, T arg)
        {
            var result = new OperationResult<TRes>();

            try
            {
                result.Data = func.Invoke(arg);
                result.IsSuccess = true;
                return result;
            }
            catch (BaseException e)
            {
                result.StatusCode = e.StatusCode;
                result.ErrorMessage = e.Message;
                return result;
            }
            catch (Exception e)
            {
                result.StatusCode = 500;
                return result;
            }
        }
    }
}
