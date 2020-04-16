﻿using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Kernel.LoggingEngine
{
    public class Logger: ILogger
    {
        private readonly IServiceProvider provider;

        public Logger(IServiceProvider provider)
        {
            this.provider = provider;
        }

        private async void AddLog(LogMessage log)
        {
            var bus = provider.GetRequiredService<IBus>();

            var uri = new Uri("rabbitmq://localhost/DatabaseService_Logs");

            var endpont = await bus.GetSendEndpoint(uri);
            await endpont.Send(log);
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

            var message = new LogMessage
            {
                Id = Guid.NewGuid(),
                ParentId = null,
                Level = logLevel,
                Message = formatter(state, exception),
                ServiceName = Assembly.GetEntryAssembly().GetName().Name,
                Time = DateTime.UtcNow
            };

            AddLog(message);
        }
    }
}
