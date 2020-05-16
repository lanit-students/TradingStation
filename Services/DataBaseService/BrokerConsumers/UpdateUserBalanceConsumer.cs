using System.Threading.Tasks;
using DataBaseService.Repositories.Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using DTO.MarketBrokerObjects;

namespace DataBaseService.BrokerConsumers
{
    public class UpdateUserBalanceConsumer : IConsumer<UserBalance>
    {
        private readonly ITradeRepository tradeRepository;

        public UpdateUserBalanceConsumer([FromServices] ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }

        private bool UpdateUser(UserBalance brokerUser)
        {
            tradeRepository.UpdateUserBalance(brokerUser);
            return true;
        }

        public async Task Consume(ConsumeContext<UserBalance> context)
        {
            var response = OperationResultWrapper.CreateResponse(UpdateUser, context.Message);

            await context.RespondAsync(response);
        }
    }
}

