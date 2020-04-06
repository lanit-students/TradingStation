using System;

namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server understood the request, but refuses to authorize it.
    /// </summary>
    public class ForbiddenException : BaseException
    {
        public override int StatusCode => 403;

        public override string Header => "Forbidden";

        /// <inheritdoc />
        public ForbiddenException() { }

        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }
    }
}
