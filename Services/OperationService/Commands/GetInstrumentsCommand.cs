using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OperationService.Interfaces;

namespace OperationService.Commands
{
    public class GetInstrumentsCommand : IGetInstrumentsCommand
    {
        private readonly IRequestClient<GetInstrumentsRequest> client;

        public GetInstrumentsCommand([FromServices] IRequestClient<GetInstrumentsRequest> client)
        {
            this.client = client;
        }

        private async Task<IEnumerable<Instrument>> GetInstruments(GetInstrumentsRequest request)
        {
            var response = await client.GetResponse<OperationResult<IEnumerable<Instrument>>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<IEnumerable<Instrument>> Execute(BrokerType broker, string token, int depth, InstrumentType instrument)
        {
            var request = new GetInstrumentsRequest()
            {
                Broker = broker,
                Token = token,
                Type = instrument,
                Depth = depth
            };

            return await GetInstruments(request);
        }
    }
}
