using DTO;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public interface ILoginCommand
    {
        Task<UserToken> Execute(UserEmailPassword data);
    }
}
