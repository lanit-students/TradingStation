using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class ConfirmUserConsumer : IConsumer<InternalConfirmUserRequest>
    {
        private readonly IUserRepository userRepository;

        public ConfirmUserConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private OperationResult ConfirmUser(InternalConfirmUserRequest request)
        {
            userRepository.ConfirmUser(request.UserEmail);

            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<InternalConfirmUserRequest> context)
        {
            var confirmResult = ConfirmUser(context.Message);

            await context.RespondAsync(confirmResult);
        }

    }
}
