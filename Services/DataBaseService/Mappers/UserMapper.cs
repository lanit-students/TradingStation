using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DTO;
using System;

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
    }
}
