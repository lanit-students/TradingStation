using System.Threading.Tasks;
using DataBaseService.Repositories.Interfaces;
using DTO;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;

namespace DataBaseService.BrokerConsumers
{
    public class GetBrokerUserConsumer : IConsumer<GetBrokerUserRequest>
    {
        private readonly ITradeRepository tradeRepository;

        public GetBrokerUserConsumer([FromServices] ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }

        private BrokerUser GetUser(GetBrokerUserRequest request)
        {
            return tradeRepository.GetBrokerUser(request);
        }

        public async Task Consume(ConsumeContext<GetBrokerUserRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetUser, context.Message);

            await context.RespondAsync(response);
        }
    }
}