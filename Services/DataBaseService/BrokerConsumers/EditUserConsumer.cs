using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class EditUserConsumer : IConsumer<InternalEditUserInfoRequest>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<EditUserConsumer> logger;

        public EditUserConsumer([FromServices] IUserRepository userRepository, [FromServices]ILogger<EditUserConsumer> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        private OperationResult EditUser(InternalEditUserInfoRequest request)
        {
            logger.LogInformation("EditUser request received from UserService");
            userRepository.EditUser(request.User, request.UserPasswords, request.UserAvatar);
            
            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<InternalEditUserInfoRequest> context)
        {
            var editResult = EditUser(context.Message);

            await context.RespondAsync(editResult);
        }
    }
}
