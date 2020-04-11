using System;
using System.Linq;

using DTO;
using Kernel.CustomExceptions;
using DataBaseService.Database;
using DataBaseService.Repositories.Interfaces;
using DataBaseService.Mappers.Interfaces;

namespace DataBaseService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserMapper mapper;
        private readonly TPlatformDbContext dbContext;

        public UserRepository(IUserMapper mapper, TPlatformDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public void CreateUser(User user, string email)
        {
            if (dbContext.UsersCredentials.Any(userCredential => userCredential.Email == email))
            {
                throw new BadRequestException("This email is already taken by someone.");
            }

            dbContext.Users.Add(mapper.MapToDbUser(user));
            dbContext.SaveChanges();
        }

        public void CreateUserCredential(UserCredential userCredential)
        {
            dbContext.UsersCredentials.Add(mapper.MapToDbUserCredential(userCredential));
            dbContext.SaveChanges();
        }

        public void CreateUserAvatar(UserAvatar userAvatar)
        {
            if (userAvatar == null) return;
            dbContext.UsersAvatars.Add(mapper.MapToDbUserAvatar(userAvatar));
            dbContext.SaveChanges();
        }

        public UserCredential GetUserCredential(string email)
        {
            var dbCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == email);

            return mapper.MapUserCredential(dbCredential);
        }

        public User GetUserById(Guid userId)
        {
            var dbUser = dbContext.Users.FirstOrDefault(uc => uc.Id == userId);
            if (dbUser == null)
            {
                throw new NotFoundException("User not found");
            }

            var email = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId).Email;
            return mapper.MapUser(dbUser, email);
        }

        public void DeleteUser(Guid userId)
        {
            var dbUserCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId);

            if (dbUserCredential is null)
            {
                throw new NotFoundException("Not found User for delete");
            }

            if(!dbUserCredential.IsActive)
            {
                throw new BadRequestException("User was deleted early or not confirmed");
            }
            dbUserCredential.IsActive = false;
            dbContext.SaveChanges();
        }

        public void EditUser(User user, PasswordHashChangeRequest password)
        {
            var dbUser = dbContext.Users.FirstOrDefault(uc => uc.Id == user.Id);
            if (dbUser != null)
            {
                dbUser.LastName = user.LastName;
                dbUser.FirstName = user.FirstName;
                dbUser.Birthday = user.Birthday;

                if (password != null)
                {
                    var dbUserCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == user.Id);

                    if (dbUserCredential != null)
                    {
                        if (dbUserCredential.PasswordHash != password.OldPasswordHash)
                        {
                            throw new ForbiddenException("Can't change password, old password is wrong");
                        }

                        dbUserCredential.PasswordHash = password.NewPasswordHash;
                    }
                    else
                    {
                        throw new NotFoundException("Not found User to change pasword");
                    }
                }

                dbContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException("Not found User to change");
            }

        }
    }
}
