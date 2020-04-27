using BrokerService.Utils;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using System.Threading.Tasks;

namespace BrokerService.BrokerConsumers
{
    public class TradeConsumer:IConsumer<InternalTradeRequest>
    {
        public Transaction Trade(InternalTradeRequest request)
        {
            return BrokerFactory.Create(request.Transaction.Broker, request.Token).Trade(request);
        }

        public async Task Consume(ConsumeContext<InternalTradeRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(Trade, context.Message);

            await context.RespondAsync(response);
        }
    }
}
