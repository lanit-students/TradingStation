using System;
using System.Net;

namespace IDeleteUserUserService.Interfaces
{
    public interface IDeleteUserCommand
    {
        HttpStatusCode Execute(Guid userId);
    }
}
