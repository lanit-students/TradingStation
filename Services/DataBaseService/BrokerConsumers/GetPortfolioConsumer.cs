using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class GetPortfolioConsumer : IConsumer<GetPortfolioRequest>
    {
        private ITradeRepository repository;

        public GetPortfolioConsumer([FromServices] ITradeRepository repository)
        {
            this.repository = repository;
        }

        private List<InstrumentData> GetPortfolio(GetPortfolioRequest request)
        {
            return repository.GetPortfolio(request);
        }

        public async Task Consume(ConsumeContext<GetPortfolioRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetPortfolio, context.Message);
            await context.RespondAsync(response);
        }
    }
}
