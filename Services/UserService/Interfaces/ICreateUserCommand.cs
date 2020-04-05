using DTO.RestRequests;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface ICreateUserCommand
    {
        Task<bool> Execute(CreateUserRequest request);
    }
}
