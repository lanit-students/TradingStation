using System.Net;

namespace IDeleteUserUserService.Interfaces
{

    public interface IDeleteUser<Guid, out Output>
    {
        HttpStatusCode Execute(Guid userId);
    }
}