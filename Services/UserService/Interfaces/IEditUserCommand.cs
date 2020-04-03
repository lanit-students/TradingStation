using DTO;
using System.Net;

namespace UserService.Interfaces
{
    public interface IEditUserCommand
    {
        void Execute(UserEmailPassword userEmailPassword);
    }
}
