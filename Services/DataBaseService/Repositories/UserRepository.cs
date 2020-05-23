using System;
using System.Linq;
using DTO;
using Kernel.CustomExceptions;
using DataBaseService.Database;
using DataBaseService.Repositories.Interfaces;
using DataBaseService.Mappers.Interfaces;
using DTO.BrokerRequests;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserMapper mapper;
        private readonly TPlatformDbContext dbContext;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(IUserMapper mapper, TPlatformDbContext dbContext, [FromServices] ILogger<UserRepository> logger)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void CreateUser(User user, string email)
        {
            if (dbContext.UsersCredentials.Any(userCredential => userCredential.Email == email))
            {
                var e = new BadRequestException($"{Guid.NewGuid()}_Email {email} is already taken by someone.");
                logger.LogWarning(e, e.Message);
                throw e;
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

            if (dbCredential == null)
            {
                var e = new NotFoundException($"{Guid.NewGuid()}_User with email {email} not found");
                logger.LogWarning(e, e.Message);
                throw e;
            }

            if(!dbCredential.IsActive)
            {
                var e = new ForbiddenException("User wasn't confirm or delete");
                logger.LogWarning(e, "GetUserCredential: User {1} wasn't confirm or delete", email);
                throw e;
            }

            return mapper.MapUserCredential(dbCredential);
        }

        public InternalGetUserByIdResponse GetUserWithAvatarById(Guid userId)
        {
            var dbUser = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (dbUser == null)
            {
                var e = new NotFoundException($"{Guid.NewGuid()}_User not found");
                logger.LogWarning(e, e.Message);
                throw e;
            }
            var email = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId).Email;

            var user = mapper.MapUser(dbUser, email);

            UserAvatar userAvatar = null;
            var dbUserAvatar = dbContext.UsersAvatars.FirstOrDefault(ua => ua.UserId == userId);
            if (dbUserAvatar != null && dbUserAvatar.Avatar != null)
                userAvatar = mapper.MapUserAvatar(dbUserAvatar);

            logger.LogInformation($"GetUserWithAvatarById: User with id {userId} was found and sent to UserService");
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
                var e = new NotFoundException($"{Guid.NewGuid()}_Not found user to delete");
                logger.LogWarning(e, $"{e.Message}, user id: {userId}");
                throw e;
            }

            if(!dbUserCredential.IsActive)
            {
                var e = new BadRequestException($"{Guid.NewGuid()}_User was deleted earlier or not confirmed");
                logger.LogWarning(e, $"{e.Message}, user id: {userId}");
                throw e;
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
                            var e = new ForbiddenException($"{Guid.NewGuid()}_Can't change password of {user.Email}, old password is wrong");
                            logger.LogWarning(e, e.Message);
                            throw e;
                        }

                        dbUserCredential.PasswordHash = password.NewPasswordHash;
                    }
                    else
                    {
                        var e = new NotFoundException($"{Guid.NewGuid()}_Not found user {user.Email} to change password");
                        logger.LogWarning(e, e.Message);
                        throw e;
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
                logger.LogInformation($"EditUser: Information about user with id {user.Id} was changed");
            }
            else
            {
                var e = new NotFoundException($"{Guid.NewGuid()}_Not found user {user.Email} to change information");
                logger.LogWarning(e, e.Message);
                throw e;
            }
        }
        public void ConfirmUser(string Email)
        {
            var dbUserCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == Email);

            if (dbUserCredential is null)
            {
                var exception = new NotFoundException("Not found User for confirm");
                logger.LogWarning(exception, "ConfirmUser: wasn't found");
                throw exception;
            }

            if (dbUserCredential.IsActive)
            {
                var exception = new BadRequestException("User was confirmed early");
                logger.LogWarning(exception, "ConfirmUser: was confirmed early");
                throw exception;
            }

            dbUserCredential.IsActive = true;
            dbContext.SaveChanges();
        }
    }
}
