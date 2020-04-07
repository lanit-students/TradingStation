using DTO;
using System;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface IGetUserByIdCommand
    {
        Task<User> Execute(Guid request);
    }
}
