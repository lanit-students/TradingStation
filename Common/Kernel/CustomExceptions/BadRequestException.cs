using System;

namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server cannot understand the request.
    /// </summary>
    public class BadRequestException : BaseException
    {
        public override int StatusCode => 400;

        public override string Header => "Bad Request";

        /// <inheritdoc />
        public BadRequestException() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
