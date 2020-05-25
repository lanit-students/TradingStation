using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Kernel.LoggingEngine
{
    public class Logger : ILogger
    {
        private readonly IServiceProvider provider;

        public Logger(IServiceProvider provider)
        {
            this.provider = provider;
        }

        private async Task AddLog(LogMessage log)
        {
            var bus = provider.GetRequiredService<IBus>();

            var uri = new Uri("rabbitmq://localhost/DatabaseService_Logs");

            var endpoint = await bus.GetSendEndpoint(uri);
            await endpoint.Send(log);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null)
                return;

            var data = ErrorMessageFormatter.GetMessageData(formatter(state, exception));

            var message = new LogMessage
            {
                Id = data.Item1 ?? Guid.NewGuid(),
                ParentId = data.Item2,
                Level = logLevel,
                Message = data.Item3,
                ServiceName = Assembly.GetEntryAssembly().GetName().Name,
                Time = DateTime.UtcNow
            };

            AddLog(message);
        }
    }
}
