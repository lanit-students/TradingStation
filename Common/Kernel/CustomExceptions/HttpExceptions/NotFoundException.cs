namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Means that the requested resource may be available in the future, which, however, does not guarantee the availability of previous content
    /// </summary>
    public class NotFoundException : CustomHttpExceptionBase
    {
        public override int StatusCode => 404;
        private string errorMessage = "Not found";

        public override string Message => errorMessage;

        /// Need for unit tests
        public NotFoundException() { }

        public NotFoundException(string message) { errorMessage = message; }
    }
}
