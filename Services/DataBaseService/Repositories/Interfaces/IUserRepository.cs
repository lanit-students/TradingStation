using DTO;
using System;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);

        void CreateUserCredential(UserCredential userCredential);

        UserCredential GetUserCredential(string email);

        User GetUserById(Guid userId);

        void DeleteUser(Guid userIdCredential);
    }
}
