using System.Threading.Tasks;
using DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OperationService.Hubs;

namespace OperationService.BrokerConsumers
{
    public class CandleConsumer : IConsumer<Candle>
    {
        private readonly IHubContext<CandleHub> hubContext;

        public CandleConsumer([FromServices] IHubContext<CandleHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<Candle> context)
        {
            await hubContext
                .Clients.Group(context.Message.Figi)
                .SendCoreAsync("ReceiveMessage", new object[] {context.Message});
        }
    }
}
