﻿using System;
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
                var exception = new BadRequestException("This email is already taken by someone.");
                logger.LogWarning(exception, "CreateUser: This email {1} is already taken by someone.", user.Email);
                throw exception;
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
                var exception = new NotFoundException("User with given email not found");
                logger.LogWarning(exception, "GetUserCredential: User with given email {1} not found.", email);
                throw exception;
            }
            if(!dbCredential.IsActive)
            {
                var exception = new ForbiddenException("User wasn't confirm or delete");
                logger.LogWarning(exception, "GetUserCredential: User {1} wasn't confirm or delete", email);
                throw exception;
            }
            return mapper.MapUserCredential(dbCredential);
        }

        public InternalGetUserByIdResponse GetUserWithAvatarById(Guid userId)
        {
            var dbUser = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (dbUser == null)
            {
                var exception = new NotFoundException("User not found");
                logger.LogWarning(exception, "GetUserWithAvatarById: User with id {1} wasn't found ", userId);
                throw exception;
            }
            var email = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId).Email;

            var user = mapper.MapUser(dbUser, email);

            UserAvatar userAvatar = null;
            var dbUserAvatar = dbContext.UsersAvatars.FirstOrDefault(ua => ua.UserId == userId);
            if (dbUserAvatar != null && dbUserAvatar.Avatar != null)
                userAvatar = mapper.MapUserAvatar(dbUserAvatar);

            logger.LogInformation("GetUserWithAvatarById: User with id {1} was found and send to UserService", userId);
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
                var exception = new NotFoundException("Not found User for delete");
                logger.LogWarning(exception, "DeleteUser: Not found User for delete");
                throw exception;
            }

            if(!dbUserCredential.IsActive)
            {
                var exception = new BadRequestException("User was deleted early or not confirmed");
                logger.LogWarning(exception, "DeleteUser: User was deleted early or not confirmed");
                throw exception;
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
                            var exception = new ForbiddenException("Can't change password, old password is wrong");
                            logger.LogWarning(exception,
                                "Edit User with id {1} operation was stopped: old password is wrong ", user.Id);
                            throw exception;
                        }

                        dbUserCredential.PasswordHash = password.NewPasswordHash;
                    }
                    else
                    {
                        var exception = new NotFoundException("Not found user to change password");
                        logger.LogWarning(exception, "EditUser: User with id {1} wasn't found in UserCredentials table", user.Id);
                        throw exception;
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
                logger.LogInformation("EditUser: Information about user with id {1} was changed ", user.Id);
            }
            else
            {
                var exception = new NotFoundException("User to change information not found in Users table");
                logger.LogWarning(exception, "EditUser: User with id {1} wasn't found in Users table", user.Id);
                throw exception;
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
