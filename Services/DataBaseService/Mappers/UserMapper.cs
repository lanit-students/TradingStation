using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DTO;

namespace DataBaseService.Mappers
{
    public class UserMapper : IUserMapper
    {
        public DbUser MapToDbUser(User user)
        {
            return new DbUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday
            };
        }

        public DbUserCredential MapToDbUserCredential(UserCredential userCredential)
        {
            return new DbUserCredential
            {
                Id = userCredential.Id,
                UserId = userCredential.UserId,
                Email = userCredential.Email,
                PasswordHash = userCredential.PasswordHash
            };
        }

        public DbUsersAvatars MapToDbUserAvatar(UserAvatar userAvatar)
        {
            return new DbUsersAvatars
            {
                Id = userAvatar.Id,
                Avatar = userAvatar.Avatar,
                AvatarExtension = userAvatar.AvatarExtension,
                UserId = userAvatar.UserId
            };
        }

        public User MapUser(DbUser dbUser, string email)
        {
            return new User
            {
                Id = dbUser.Id,
                Email = email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Birthday = dbUser.Birthday
            };
        }

        public UserCredential MapUserCredential(DbUserCredential dbUserCredential)
        {
            return new UserCredential
            {
                Id = dbUserCredential.Id,
                UserId = dbUserCredential.UserId,
                Email = dbUserCredential.Email,
                PasswordHash = dbUserCredential.PasswordHash
            };
        }

        public UserAvatar MapUserAvatar(DbUsersAvatars userAvatar)
        {
            return new UserAvatar
            {
                Id = userAvatar.Id,
                Avatar = userAvatar.Avatar,
                AvatarExtension = userAvatar.AvatarExtension,
                UserId = userAvatar.UserId
            };
        }
    }
}