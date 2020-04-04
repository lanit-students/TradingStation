using System;

namespace IDeleteUserUserService.Interfaces
{
    public interface IDeleteUserCommand
    {
        bool Execute(Guid userId);
    }
}
