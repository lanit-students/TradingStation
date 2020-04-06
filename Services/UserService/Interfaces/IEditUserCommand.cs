using DTO.RestRequests;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IEditUserCommand
    {
        Task<bool> Execute(CreateUserRequest request, string newPassword);
    }
}
