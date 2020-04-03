using DTO;
using System;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface ICreateUserCommand
    {
        Task<Guid> Execute(UserEmailPassword userEmailPassword);
    }
}
