using DTO;
using System.Net;

namespace UserService.Interfaces
{
    public interface ICreateUserCommand
    {
        HttpStatusCode Execute(UserCredential userCredential);
    }
}
