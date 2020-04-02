using System;

namespace CustomException
{
    /// <summary>
    /// Indicates that the server understood the request, but refuses to authorize it
    /// </summary>
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
