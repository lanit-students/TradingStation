using DTO;
using System.Net;

namespace UserService.Interfaces
{
    public interface ICreateUserCommand
    {
        void Execute(UserEmailPassword userEmailPassword);
    }
}
