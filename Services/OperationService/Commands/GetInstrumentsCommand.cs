using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OperationService.Commands
{
    public class GetInstrumentsCommand : ICommand<GetInstrumentsRequest, IEnumerable<Instrument>>
    {
        private readonly IRequestClient<GetInstrumentsRequest> client;
        private readonly ILogger<GetInstrumentsCommand> logger;

        public GetInstrumentsCommand(
            [FromServices] IRequestClient<GetInstrumentsRequest> client,
            [FromServices] ILogger<GetInstrumentsCommand> logger
            )
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<IEnumerable<Instrument>> GetInstruments(GetInstrumentsRequest request)
        {
            var response = await client.GetResponse<OperationResult<IEnumerable<Instrument>>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<IEnumerable<Instrument>> Execute(GetInstrumentsRequest request)
        {
            var result = await GetInstruments(request);
            logger.LogInformation($"Instruments from {request.Broker} received successfully");
            return result;
        }
    }
}
