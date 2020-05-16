using DataBaseService.Repositories.Interfaces;
using DTO.BrokerRequests;
using Kernel;
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

        private bool ConfirmUser(InternalConfirmUserRequest request)
        {
            userRepository.ConfirmUser(request.UserEmail);

            return true;
        }

        public async Task Consume(ConsumeContext<InternalConfirmUserRequest> context)
        {
            var confirmResult = OperationResultWrapper.CreateResponse(ConfirmUser,context.Message);

            await context.RespondAsync(confirmResult);
        }

    }
}
