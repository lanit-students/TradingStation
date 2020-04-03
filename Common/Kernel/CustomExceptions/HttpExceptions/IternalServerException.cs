using System.Runtime.CompilerServices;

namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server detected an unexpected condition that prevented it from completing the request
    /// </summary>
    public class IternalServerException : BaseCustomHttpException
    {
        public override int StatusCode => 500;
        public override string Header => "Internal Server Error";

        /// Need for unit tests
        public IternalServerException() { }

        public IternalServerException(string message) : base(message) { }
    }
}
