using DTO;
using System;
using DTO.BrokerRequests;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user, string email);

        void CreateUserCredential(UserCredential userCredential);

        void CreateUserAvatar(UserAvatar userAvatar);

        UserCredential GetUserCredential(string email);

        InternalGetUserByIdResponse GetUserWithAvatarById(Guid userId);

        void DeleteUser(Guid userIdCredential);

        void EditUser(User user, PasswordHashChangeRequest password);
    }
}
