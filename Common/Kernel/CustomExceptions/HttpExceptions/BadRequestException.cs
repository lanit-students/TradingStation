namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server cannot understand the request due to incorrect syntax
    /// </summary>
    public class BadRequestException : CustomHttpExceptionBase
    {
        public override int StatusCode { get => 400; }
        private string errorMessage = "400 Bad Request";

        public override string Message { get => errorMessage; }

        public BadRequestException() { }

        public BadRequestException(string message) { errorMessage = message; }
    }
}
