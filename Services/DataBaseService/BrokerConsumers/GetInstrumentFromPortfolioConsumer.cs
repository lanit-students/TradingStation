using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class GetInstrumentFromPortfolioConsumer : IConsumer<GetInstrumentFromPortfolioRequest>
    {
        private readonly ITradeRepository tradeRepository;

        public GetInstrumentFromPortfolioConsumer([FromServices] ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }

        private Instrument GetInstrument(GetInstrumentFromPortfolioRequest request)
        {
            return tradeRepository.GetInstrumentFromPortfolio(request);
        }

        public async Task Consume(ConsumeContext<GetInstrumentFromPortfolioRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetInstrument, context.Message);

            await context.RespondAsync(response);
        }
}
}
