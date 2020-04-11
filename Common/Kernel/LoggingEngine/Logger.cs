using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kernel.LoggingEngine
{
    public class Logger: ILogger
    {
        private readonly IBus busControl;
        private static object _lock = new object();

        public Logger([FromServices]IBus busControl)
        {
            this.busControl = busControl;
        }

        private async void AddLog(Log log)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            busControl.Publish<Log>(uri).Start();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {

                }
            }
        }
    }
}
