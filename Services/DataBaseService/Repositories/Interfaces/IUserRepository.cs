using DTO;
using System;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user, string email);

        void CreateUserCredential(UserCredential userCredential);

        void CreateUserAvatar(UserAvatar userAvatar);

        UserCredential GetUserCredential(string email);

        User GetUserById(Guid userId);

        void DeleteUser(Guid userIdCredential);

        void EditUser(User user, PasswordHashChangeRequest password);
    }
}
