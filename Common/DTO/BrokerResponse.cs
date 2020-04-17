using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BrokerResponse<T>
    {
        public T Response { get; set; }

        public int StatusCode { get; set; } = 200;

        public string Message { get; set; }
    }
}
