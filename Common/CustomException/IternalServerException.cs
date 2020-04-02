using System;

namespace CustomException
{
    /// <summary>
    /// Indicates that the server detected an unexpected condition that prevented it from completing the request
    /// </summary>
    public class IternalServerException : Exception
    {
        public IternalServerException(string message)
            : base(message)
        { }

        public IternalServerException()
        { }

        public IternalServerException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
