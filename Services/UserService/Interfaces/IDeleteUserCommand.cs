using DTO.RestRequests;
using System.Threading.Tasks;

namespace IDeleteUserUserService.Interfaces
{
    public interface IDeleteUserCommand
    {
        Task<bool> Execute(DeleteUserRequest request);
    }
}
