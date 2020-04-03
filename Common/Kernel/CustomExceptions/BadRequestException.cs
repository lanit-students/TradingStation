namespace Kernel.CustomExceptions
{
    /// <summary>
    /// Indicates that the server cannot understand the request.
    /// </summary>
    public class BadRequestException : BaseException
    {
        public override int StatusCode => 400;
        
        public override string Header => "Bad Request";
    }
}
