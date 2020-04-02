using System;

namespace CustomException
{
    /// <summary>
    /// Indicates that the server cannot understand the request due to incorrect syntax
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        { }

        public BadRequestException()
        { }

        public BadRequestException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
