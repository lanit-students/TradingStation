using System;

namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Means that the requested resource may be available in the future, which, however, does not guarantee the availability of previous content.
    /// </summary>
    public class NotFoundException : BaseException
    {
        public override int StatusCode => 404;

        public override string Header => "Not found";

        /// <inheritdoc />
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
