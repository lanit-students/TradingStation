﻿namespace Kernel.CustomExceptions
{
    public class IternalServerException : BaseException
    {
        public override int StatusCode => 500;

        public override string Header => "Internal Server Error";

        /// <inheritdoc />
        public IternalServerException() { }

        public IternalServerException(string message) : base(message) { }
    }
}
