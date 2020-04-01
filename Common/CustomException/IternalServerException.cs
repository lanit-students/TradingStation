using System;
using System.Collections.Generic;
using System.Text;

namespace CustomException
{
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
