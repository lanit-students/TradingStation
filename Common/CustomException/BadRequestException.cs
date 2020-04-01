using System;

namespace CustomException
{
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
