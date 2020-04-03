using System;

namespace IDeleteUserUserService.Interfaces
{
    public interface IDeleteUserCommand
    {
        void Execute(Guid userId);
    }
}
