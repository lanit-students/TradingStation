using System;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IConfirmUserCommand
    {
        Task<string> Execute(Guid secretToken);
    }
}
