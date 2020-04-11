using DataBaseService.Database.Logs.Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class AddLogConsumer : IConsumer<LogMessage>
    {
        private readonly ILogRepository logRepository;

        public AddLogConsumer([FromServices] ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public Task Consume(ConsumeContext<LogMessage> context)
        {
            logRepository.Save(context.Message);

            return Task.FromResult(0);
        }
    }
}
