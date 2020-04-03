namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server understood the request, but refuses to authorize it
    /// </summary>
    public class ForbiddenException : CustomHttpExceptionBase
    {
        public override int StatusCode => 403;
        private string errorMessage = "Forbidden";

        public override string Message => errorMessage;

        /// Need for unit tests
        public ForbiddenException() { }

        public ForbiddenException(string message) { errorMessage = message; }
    }
}
