using System;

namespace Kernel.CustomExceptions
{
    public class InternalServerException : BaseException
    {
        public override int StatusCode => 500;

        public override string Header => "Internal Server Error";

        /// <inheritdoc />
        public InternalServerException() { }

        public InternalServerException(string message) : base(message) { }

        public InternalServerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
