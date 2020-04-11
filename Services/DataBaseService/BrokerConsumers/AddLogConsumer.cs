using DataBaseService.Database.Logs.Interfaces;
using DTO;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class AddLogConsumer : IConsumer<Log>
    {
        private readonly ILogRepository logRepository;

        public AddLogConsumer([FromServices] ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        private OperationResult AddLog(Log log)
        {
            logRepository.AddLogs(log);

            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<Log> context)
        {
            var addResult = AddLog(context.Message);

            await context.RespondAsync(addResult);
        }
    }
}
