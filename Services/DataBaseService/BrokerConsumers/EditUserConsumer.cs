using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class EditUserConsumer : IConsumer<InternalEditUserInfoRequest>
    {
        private readonly IUserRepository userRepository;

        public EditUserConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private bool EditUser(InternalEditUserInfoRequest request)
        {
            userRepository.EditUser(request.User, request.UserPasswords);

            return true;
        }

        public async Task Consume(ConsumeContext<InternalEditUserInfoRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(EditUser, context.Message);

            await context.RespondAsync(response);
        }
    }
}
