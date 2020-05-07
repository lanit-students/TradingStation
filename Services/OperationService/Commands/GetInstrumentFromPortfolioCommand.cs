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
    public class GetInstrumentFromPortfolioCommand : ICommand<GetInstrumentFromPortfolioRequest, Instrument>
    {
        private readonly IRequestClient<GetInstrumentFromPortfolioRequest> client;
        private readonly ILogger<GetInstrumentFromPortfolioCommand> logger;

        public GetInstrumentFromPortfolioCommand(
            [FromServices] IRequestClient<GetInstrumentFromPortfolioRequest> client,
            [FromServices] ILogger<GetInstrumentFromPortfolioCommand> logger
            )
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<Instrument> GetInstrument(GetInstrumentFromPortfolioRequest request)
        {
            var response = await client.GetResponse<OperationResult<Instrument>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<Instrument> Execute(GetInstrumentFromPortfolioRequest request)
        {
            var result = await GetInstrument(request);
            logger.LogInformation($"Instrument from portfolio of user {request.UserId} received successfully");
            return result;
        }
    }
}
