using Microsoft.Extensions.Logging;
using System;

namespace Kernel.LoggingEngine
{
    public class LoggerProvider: ILoggerProvider
    {
        private readonly IServiceProvider provider;

        public LoggerProvider(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(provider);
        }

        public void Dispose() { }
    }
}
