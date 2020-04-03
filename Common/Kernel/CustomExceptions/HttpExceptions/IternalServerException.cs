namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server detected an unexpected condition that prevented it from completing the request
    /// </summary>
    public class IternalServerException : CustomHttpExceptionBase
    {
        public override int StatusCode { get => 500; }
        private string errorMessage = "500 Internal Server Error";

        public override string Message { get => errorMessage; }

        public IternalServerException() { }

        public IternalServerException(string message) { errorMessage = message; }
    }
}
