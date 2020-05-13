using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Kernel;
using Kernel.CustomExceptions;

namespace OperationService.Commands
{
    public class GetPortfolioCommand : ICommand<GetPortfolioRequest, List<InstrumentData>>
    {
        private IRequestClient<GetPortfolioRequest> client;

        public GetPortfolioCommand([FromServices] IRequestClient<GetPortfolioRequest> client)
        {
            this.client = client;
        }

        private async Task<List<InstrumentData>> GetPortfolio(GetPortfolioRequest request)
        {
            var response = await client.GetResponse<OperationResult<List<InstrumentData>>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<List<InstrumentData>> Execute(GetPortfolioRequest request)
        {
            try
            {
                return await GetPortfolio(request);
            }
            catch
            {
                // tratata
                throw new InternalServerException();
            }
        }
    }
}
