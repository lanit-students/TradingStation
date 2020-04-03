using System;

namespace Kernel.CustomExceptions
{
    public abstract class CustomHttpExceptionBase : Exception
    {
        public virtual int StatusCode => 500;
        public CustomHttpExceptionBase() { }
        public CustomHttpExceptionBase(string message) : base(message) { }
    }
}
