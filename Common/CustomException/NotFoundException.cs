using System;

namespace CustomException
{
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
