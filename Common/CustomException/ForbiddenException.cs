using System;

namespace CustomException
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message)
            : base(message)
        { }

        public ForbiddenException()
        { }

        public ForbiddenException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
