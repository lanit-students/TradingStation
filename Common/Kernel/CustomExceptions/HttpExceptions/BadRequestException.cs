namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server cannot understand the request due to incorrect syntax
    /// </summary>
    public class BadRequestException : BaseCustomHttpException
    {
        public override int StatusCode => 400;
        public override string Header => "Bad Request";

        /// Need for unit tests
        public BadRequestException() { }

        public BadRequestException(string message) : base(message) { }
    }
}
