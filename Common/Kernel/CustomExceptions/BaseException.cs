using System;

namespace Kernel.CustomExceptions
{
    public abstract class BaseException : Exception
    {
        public virtual int StatusCode { get; }

        public virtual string Header { get; }

        /// <summary>
        /// Need for unit tests.
        /// </summary>
        public BaseException() { }

        public BaseException(string message) : base(message) { }

        public BaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
