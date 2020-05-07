using DataBaseService.Repositories.Interfaces;
using DTO;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class TransactionConsumer : IConsumer<Transaction>
    {
        private readonly ITradeRepository tradeRepository;

        public TransactionConsumer([FromServices] ITradeRepository tradeRepository)
        {
            this.tradeRepository = tradeRepository;
        }

        private bool SaveTransaction(Transaction transaction)
        {
            tradeRepository.SaveTransaction(transaction);
            return true;
        }

        public async Task Consume(ConsumeContext<Transaction> context)
        {
            var response = OperationResultWrapper.CreateResponse(SaveTransaction, context.Message);

            await context.RespondAsync(response);
        }
    }
}
