using System;
using System.Linq;

using DTO;
using Kernel.CustomExceptions;
using DataBaseService.Database;
using DataBaseService.Repositories.Interfaces;
using DataBaseService.Mappers.Interfaces;
using DTO.BrokerRequests;

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
            dbContext.UsersAvatars.Add(mapper.MapToDbUserAvatar(userAvatar));
            dbContext.SaveChanges();
        }

        public UserCredential GetUserCredential(string email)
        {
            var dbCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == email);

            return mapper.MapUserCredential(dbCredential);
        }

        public InternalGetUserByIdResponse GetUserWithAvatarById(Guid userId)
        {
            var dbUser = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (dbUser == null)
            {
                throw new NotFoundException("User not found");
            }
            var email = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId).Email;

            var user = mapper.MapUser(dbUser, email);

            UserAvatar userAvatar = null;
            var dbUserAvatar = dbContext.UsersAvatars.FirstOrDefault(ua => ua.UserId == userId);
            if (dbUserAvatar != null && dbUserAvatar.Avatar != null)
                userAvatar = mapper.MapUserAvatar(dbUserAvatar);

            return new InternalGetUserByIdResponse
            {
                User = user,
                UserAvatar = userAvatar
            };
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

        public void EditUser(User user, PasswordHashChangeRequest password, UserAvatar userAvatar)
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
                if (userAvatar != null)
                {
                    var dbUserAvatar = dbContext.UsersAvatars.FirstOrDefault(ua => ua.UserId == user.Id);

                    if (dbUserAvatar == null)
                    {
                        userAvatar.Id = Guid.NewGuid();
                        userAvatar.UserId = user.Id;
                        dbContext.UsersAvatars.Add(mapper.MapToDbUserAvatar(userAvatar));
                    }
                    else
                    {
                        dbUserAvatar.AvatarExtension = userAvatar.AvatarExtension;
                        dbUserAvatar.Avatar = userAvatar.Avatar;
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
