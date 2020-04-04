using System;

namespace AuthenticationService.Interfaces
{
    public interface ILogoutCommand
    {
        bool Execute(Guid userId);
    }
}
