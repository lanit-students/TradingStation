using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.LoggingEngine
{
    public class LoggerProvider: ILoggerProvider
    {
        private readonly IBus busControl;
        public LoggerProvider(string _path)
        {
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(busControl);
        }

        public void Dispose()
        {
        }
    }
}
