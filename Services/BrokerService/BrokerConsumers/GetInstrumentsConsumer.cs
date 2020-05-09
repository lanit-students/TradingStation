using BrokerService.Utils;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerService.BrokerConsumers
{
    public class GetInstrumentsConsumer : IConsumer<GetInstrumentsRequest>
    {
        public IEnumerable<Instrument> GetInstruments(GetInstrumentsRequest request)
        {
            return BrokerFactory.Create(request.Broker, request.Token).GetInstruments(request.Type);
        }

        public async Task Consume(ConsumeContext<GetInstrumentsRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetInstruments, context.Message);

            await context.RespondAsync(response);
        }
    }
}
