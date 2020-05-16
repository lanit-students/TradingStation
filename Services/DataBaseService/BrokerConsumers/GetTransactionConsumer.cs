using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseService.BrokerConsumers
{
    public class GetTransactionConsumer : IConsumer<GetUserTransactions>
    {
        private readonly ITradeRepository tradeRepository;

        public GetTransactionConsumer([FromServices] ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }

        private IEnumerable<Transaction> GetTransactions(GetUserTransactions request)
        {
            return tradeRepository.GetUserTransactions(request);
        }

        public async Task Consume(ConsumeContext<GetUserTransactions> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetTransactions, context.Message);

            await context.RespondAsync(response);
        }
    }
}
