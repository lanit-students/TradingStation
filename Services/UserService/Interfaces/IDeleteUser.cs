using System;
using System.Net;

namespace IDeleteUserUserService.Interfaces
{

    public interface IDeleteUser
    {
        HttpStatusCode Execute(int userId);
    }
}