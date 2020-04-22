using DataBaseService.Repositories.Interfaces;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class GetUserByIdConsumer : IConsumer<InternalGetUserByIdRequest>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<GetUserByIdConsumer> logger;

        public GetUserByIdConsumer
            ([FromServices] IUserRepository userRepository,
            [FromServices] ILogger<GetUserByIdConsumer> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        private InternalGetUserByIdResponse GetUserById(InternalGetUserByIdRequest request)
        {
            logger.LogInformation("GetUserById request received from UserService");
            return userRepository.GetUserWithAvatarById(request.UserId);
        }

        public async Task Consume(ConsumeContext<InternalGetUserByIdRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetUserById, context.Message);

            await context.RespondAsync(response);
        }
    }
}
