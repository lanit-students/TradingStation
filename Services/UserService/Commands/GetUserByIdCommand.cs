using DTO.BrokerRequests;
using DTO.RestRequests;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Microsoft.Extensions.Logging;
using DTO;
using Kernel;

namespace UserService.Commands
{
    public class GetUserByIdCommand : IGetUserByIdCommand
    {
        private readonly IRequestClient<InternalGetUserByIdRequest> client;
        private readonly ILogger<GetUserByIdCommand> logger;

        public GetUserByIdCommand
            ([FromServices]IRequestClient<InternalGetUserByIdRequest> client,
            [FromServices] ILogger<GetUserByIdCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<InternalGetUserByIdResponse> GetUserById(InternalGetUserByIdRequest request)
        {
            logger.LogInformation("Response from Database Service GetUserById method received");
            var response = await client.GetResponse<OperationResult<InternalGetUserByIdResponse>>(request);
            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<UserInfoRequest> Execute(Guid request)
        {
            var internalRequest = new InternalGetUserByIdRequest { UserId = request };

            var internalResponse = await GetUserById(internalRequest);

            byte[] avatar = null;
            string avatarExtension = null;

            var userAvatar = internalResponse.UserAvatar;
            var user = internalResponse.User;
            if (userAvatar != null)
            {
                avatar = userAvatar.Avatar;
                avatarExtension = userAvatar.AvatarExtension;
            }
            var restResponse = new UserInfoRequest
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Avatar = avatar,
                AvatarExtension = avatarExtension,
            };
            return restResponse;
        }
    }
}
