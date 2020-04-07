using DTO.RestRequests;
using System;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IEditUserCommand
    {
        Task<bool> Execute(CreateUserRequest request, string newPassword, Guid id);
    }
}
