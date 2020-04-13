using DTO.BrokerRequests;
using DTO.RestRequests;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MassTransit;

namespace UserService.Commands
{
    public class GetUserByIdCommand : IGetUserByIdCommand
    {
        private readonly IRequestClient<InternalGetUserByIdRequest> client;

        public GetUserByIdCommand([FromServices]IRequestClient<InternalGetUserByIdRequest> client)
        {
            this.client = client;
        }

        private async Task<InternalGetUserByIdResponse> GetUserById(InternalGetUserByIdRequest request)
        {
            var result = await client.GetResponse<InternalGetUserByIdResponse>(request);

            return result.Message;
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
