using DTO;
using DTO.RestRequests;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public interface ILoginCommand
    {
        Task<UserToken> Execute(LoginRequest request);
    }
}
