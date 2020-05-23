using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Kernel;
using Kernel.CustomExceptions;
using Microsoft.Extensions.Logging;
using System;

namespace OperationService.Commands
{
    public class GetPortfolioCommand : ICommand<GetPortfolioRequest, List<InstrumentData>>
    {
        private IRequestClient<GetPortfolioRequest> client;
        private ILogger<GetPortfolioCommand> logger;

        public GetPortfolioCommand(
            [FromServices] IRequestClient<GetPortfolioRequest> client,
            [FromServices] ILogger<GetPortfolioCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<List<InstrumentData>> GetPortfolio(GetPortfolioRequest request)
        {
            var response = await client.GetResponse<OperationResult<List<InstrumentData>>>(request);

            logger.LogInformation($"User {request.UserId} requested portfolio.");

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<List<InstrumentData>> Execute(GetPortfolioRequest request)
        {
            try
            {
                return await GetPortfolio(request);
            }
            catch (NotFoundException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);
                var ex = new NotFoundException(errorData.Item3);
                logger.LogWarning(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }
        }
    }
}
