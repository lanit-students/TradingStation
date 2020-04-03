namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server understood the request, but refuses to authorize it
    /// </summary>
    public class ForbiddenException : BaseCustomHttpException
    {
        public override int StatusCode => 403;
        public override string Header => "Forbidden";

        /// Need for unit tests
        public ForbiddenException() { }

        public ForbiddenException(string message) : base(message) { }
    }
}
