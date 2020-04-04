using System;
using DTO;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);

        void CreateUserCredential(UserCredential userCredential);

        UserCredential GetUserCredential(string email);

        UserCredential GetUserCredentialById(Guid Id);
    }
}
