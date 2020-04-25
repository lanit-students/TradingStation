using System.Threading.Tasks;
using DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OperationService.Hubs;

namespace OperationService.BrokerConsumers
{
    public class CandleConsumer : IConsumer<Candle>
    {
        private readonly CandleHub candleHub;

        public CandleConsumer([FromServices] CandleHub candleHub)
        {
            this.candleHub = candleHub;
        }

        public Task Consume(ConsumeContext<Candle> context)
        {
            return candleHub.SendMessage(context.Message.Figi, context.Message);
        }
    }
}
