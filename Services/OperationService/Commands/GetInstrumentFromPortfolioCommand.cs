using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace OperationService.Commands
{
    public class GetInstrumentFromPortfolioCommand : ICommand<GetInstrumentFromPortfolioRequest, Instrument>
    {
        private readonly IRequestClient<GetInstrumentFromPortfolioRequest> client;

        public GetInstrumentFromPortfolioCommand(
            [FromServices] IRequestClient<GetInstrumentFromPortfolioRequest> client
            )
        {
            this.client = client;
        }

        private async Task<Instrument> GetInstrument(GetInstrumentFromPortfolioRequest request)
        {
            var response = await client.GetResponse<OperationResult<Instrument>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<Instrument> Execute(GetInstrumentFromPortfolioRequest request)
        {
            return await GetInstrument(request);
        }
    }
}
