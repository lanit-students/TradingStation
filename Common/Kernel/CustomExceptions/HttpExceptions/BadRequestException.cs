namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server cannot understand the request due to incorrect syntax
    /// </summary>
    public class BadRequestException : CustomHttpExceptionBase
    {
        public override int StatusCode => 400;
        private string errorMessage = "Bad Request";

        public override string Message => errorMessage;

        /// Need for unit tests
        public BadRequestException() { }

        public BadRequestException(string message) { errorMessage = message; }
    }
}
