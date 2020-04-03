namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server detected an unexpected condition that prevented it from completing the request
    /// </summary>
    public class IternalServerException : CustomHttpExceptionBase
    {
        public override int StatusCode => 500;
        private string errorMessage = "Internal Server Error";

        public override string Message => errorMessage;

        /// Need for unit tests
        public IternalServerException() { }

        public IternalServerException(string message) { errorMessage = message; }
    }
}
