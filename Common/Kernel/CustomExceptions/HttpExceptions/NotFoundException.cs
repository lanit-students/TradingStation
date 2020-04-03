namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Means that the requested resource may be available in the future, which, however, does not guarantee the availability of previous content
    /// </summary>
    public class NotFoundException : BaseCustomHttpException
    {
        public override int StatusCode => 404;
        public override string Header => "Not Found";

        /// Need for unit tests
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }
    }
}
