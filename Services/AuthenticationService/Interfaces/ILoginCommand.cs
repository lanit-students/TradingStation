using DTO;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public interface ILoginCommand
    {
        Task<string> Execute(UserEmailPassword data);
    }
}
