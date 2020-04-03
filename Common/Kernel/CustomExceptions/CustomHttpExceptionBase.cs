using System;

namespace Kernel.CustomExceptions
{
    public abstract class CustomHttpExceptionBase : Exception
    {
        public virtual int StatusCode { get => 500; }
    }
}
