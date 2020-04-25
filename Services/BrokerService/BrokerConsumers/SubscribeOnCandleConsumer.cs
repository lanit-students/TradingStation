using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokerService.Utils;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BrokerService.BrokerConsumers
{
    public class SubscribeOnCandleConsumer : IConsumer<SubscribeOnCandleRequest>
    {
        private readonly ISendEndpoint endpoint;

        public SubscribeOnCandleConsumer([FromServices] IBus bus)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService_Logs");
            var endpont = bus.GetSendEndpoint(uri);
        }

        public OperationResult SubscribeOnCandle(SubscribeOnCandleRequest request)
        {
            BrokerFactory.Create(request.Broker, request.Token).SubscribeOnCandle(request.Figi, SendCandle);

            return new OperationResult {IsSuccess = true};
        }

        private void SendCandle(Candle candle)
        {
            endpoint.Send(candle);
        }

        public async Task Consume(ConsumeContext<SubscribeOnCandleRequest> context)
        {
            var result = OperationResultWrapper.CreateResponse(SubscribeOnCandle, context.Message);

            await context.RespondAsync(result);
        }
    }
}
