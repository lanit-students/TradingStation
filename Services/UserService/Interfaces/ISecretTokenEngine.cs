using System;

namespace UserService.Interfaces
{
    public interface ISecretTokenEngine
    {
        Guid GetToken(string email);
        string GetEmail(Guid token);
    }
}
