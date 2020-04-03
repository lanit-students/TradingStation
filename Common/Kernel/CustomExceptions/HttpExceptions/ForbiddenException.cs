namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server understood the request, but refuses to authorize it
    /// </summary>
    public class ForbiddenException : CustomHttpExceptionBase
    {
        public override int StatusCode { get => 403; }
        private string errorMessage = "403 Forbidden";

        public override string Message { get => errorMessage; }

        public ForbiddenException() { }

        public ForbiddenException(string message) { errorMessage = message; }
    }
}
