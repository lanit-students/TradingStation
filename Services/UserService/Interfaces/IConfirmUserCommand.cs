using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IConfirmUserCommand
    {
        Task<bool> Execute(string secretToken);
    }
}
