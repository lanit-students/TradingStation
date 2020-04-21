using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace OperationService.Commands
{
    public class GetInstrumentsCommand : ICommand<GetInstrumentsRequest, IEnumerable<Instrument>>
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

        public async Task<IEnumerable<Instrument>> Execute(GetInstrumentsRequest request)
        {
            return await GetInstruments(request);
        }
    }
}
