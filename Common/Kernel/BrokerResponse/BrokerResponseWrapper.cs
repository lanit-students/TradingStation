using DTO;
using Kernel.CustomExceptions;
using MassTransit.NewIdProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel
{
    public static class BrokerResponseWrapper
    {
        public static BrokerResponse<TRes> CreateResponse<T, TRes>(Func<T, TRes> func, T arg)
        {
            var response = new BrokerResponse<TRes>();

            try
            {
                response.Response = func.Invoke(arg);
                return response;
            }
            catch (BaseException e)
            {
                response.StatusCode = e.StatusCode;
                response.Message = e.Message;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
