using DTO;
using Kernel.CustomExceptions;
using MassTransit.NewIdProviders;
using System;
using System.Collections.Generic;
using System.Text;

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
                return result;
            }
            catch (BaseException e)
            {
                result.StatusCode = e.StatusCode;
                result.ErrorMessage = e.Message;
                return result;
            }
            catch (Exception)
            {
                result.StatusCode = 500;
                return result;
            }
        }
    }
}
