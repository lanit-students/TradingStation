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
    public class GetCandlesCommand : ICommand<GetCandlesRequest, IEnumerable<Candle>>
    {
        private readonly IRequestClient<GetCandlesRequest> client;
        public GetCandlesCommand([FromServices] IRequestClient<GetCandlesRequest> client)
        {
            this.client = client;
        }

        private async Task<IEnumerable<Candle>> SubscribeOnCandle(GetCandlesRequest request)
        {
            var response = await client.GetResponse<OperationResult<IEnumerable<Candle>>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<IEnumerable<Candle>> Execute(GetCandlesRequest request)
        {
            return await SubscribeOnCandle(request);
        }
    }
}
