using System;

namespace Kernel.CustomExceptions
{
    public abstract class BaseCustomHttpException : Exception
    {
        public virtual int StatusCode => 500;
        public virtual string Header => "Header";

        public BaseCustomHttpException() { }

        public BaseCustomHttpException(string message) : base(message) { }
    }
}
