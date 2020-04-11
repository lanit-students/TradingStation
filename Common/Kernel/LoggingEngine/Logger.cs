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

        public Logger(IBus busControl)
        {
            this.busControl = busControl;
        }

        private async Task<Log> AddLog(Log log)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = busControl.CreateRequestClient<Log>(uri).Create(log);

            var response = await client.GetResponse<Log>();

            return response.Message;
        }

        public async Task<Log> Execute(Log log)
        {
            var user = await AddLog(log);

            return user;
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
