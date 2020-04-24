using DTO.RestRequests;
using System;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IGetUserByIdCommand
    {
        Task<UserInfoRequest> Execute(Guid request);
    }
}
