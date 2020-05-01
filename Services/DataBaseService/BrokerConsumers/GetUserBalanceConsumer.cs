using System.Threading.Tasks;
using DataBaseService.Repositories.Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;

namespace DataBaseService.BrokerConsumers
{
    public class GetUserBalanceConsumer : IConsumer<GetUserBalanceRequest>
    {
        private readonly ITradeRepository tradeRepository;

        public GetUserBalanceConsumer([FromServices] ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }

        private UserBalance GetUser(GetUserBalanceRequest request)
        {
            return tradeRepository.GetUserBalance(request);
        }

        public async Task Consume(ConsumeContext<GetUserBalanceRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetUser, context.Message);

            await context.RespondAsync(response);
        }
    }
}