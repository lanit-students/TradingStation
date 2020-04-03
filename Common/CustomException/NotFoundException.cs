using System;

namespace CustomException
{
    /// <summary>
    /// Means that the requested resource may be available in the future, which, however, does not guarantee the availability of previous content
    /// </summary>
   public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        { }

        public NotFoundException()
        { }

        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
