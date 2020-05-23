using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokerService.Utils;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BrokerService.BrokerConsumers
{
    public class SubscribeOnCandleConsumer : IConsumer<GetCandlesRequest>
    {
        private readonly ISendEndpoint endpoint;
        public SubscribeOnCandleConsumer([FromServices] IBus bus)
        {
            var uri = new Uri("rabbitmq://localhost/OperationService");
            this.endpoint = bus.GetSendEndpoint(uri).Result;
        }

        public IEnumerable<Candle> SubscribeOnCandle(GetCandlesRequest request)
        {
            return BrokerFactory.Create(request.Broker, request.Token).SubscribeOnCandle(request.Figi, request.Interval, SendCandle);
        }

        private void SendCandle(Candle candle)
        {
            endpoint.Send(candle);
        }

        public async Task Consume(ConsumeContext<GetCandlesRequest> context)
        {
            var result = OperationResultWrapper.CreateResponse(SubscribeOnCandle, context.Message);

            await context.RespondAsync(result);
        }
    }
}
