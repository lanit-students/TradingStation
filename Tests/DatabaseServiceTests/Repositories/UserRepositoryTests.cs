using System;
using System.Linq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using DTO;
using Kernel.CustomExceptions;
using DataBaseService.Database;
using DataBaseService.Database.Models;
using DataBaseService.Mappers;
using DataBaseService.Mappers.Interfaces;
using DataBaseService.Repositories;
using DataBaseService.Repositories.Interfaces;
using DatabaseServiceTests.Comparators;
using Moq;
using Microsoft.Extensions.Logging;

namespace DatabaseServiceTests.Repositories
{
    public class UserRepositoryTests
    {
        #region Common

        private Guid userId = Guid.NewGuid();
        private Guid credentialId = Guid.NewGuid();

        private IUserRepository repository;
        private IUserMapper mapper;

        #endregion

        #region Create user test

        private DbContextOptions<TPlatformDbContext> dbOptionsCreateUser;
        private DbContextOptions<TPlatformDbContext> dbOptionsCreateCredential;
        private DbContextOptions<TPlatformDbContext> dbOptionsCreateAvatar;

        private string firstName = "Adam";
        private string lastName = "Yablokov";
        private string email = "example@gmail.com";
        private DateTime birth = DateTime.MinValue;
        private string passwordHash = "passwordHash";
        private string avatarExtension = "jpeg";
        private byte[] avatar = { 0, 0, 0, 25 };

        private User user;
        private DbUser dbUser;
        private UserCredential credential;
        private DbUserCredential dbCredential;
        private UserAvatar userAvatar;
        private DbUsersAvatars dbUserAvatar;

        #endregion

        #region Delete user tests

        private DbUserCredential dbUserCredential;
        private Mock<ILogger<UserRepository>> logger;

        #endregion

        [OneTimeSetUp]
        public void Initialize()
        {
            #region Common

            mapper = new UserMapper();
            logger = new Mock<ILogger<UserRepository>>();

            #endregion

            #region Create user tests

            user = new User
            {
                Id = userId,
                FirstName = firstName,
                LastName = lastName,
                Birthday = birth,
                Email = email
            };

            dbUser = new DbUser
            {
                Id = userId,
                FirstName = firstName,
                LastName = lastName,
                Birthday = birth
            };

            credential = new UserCredential()
            {
                Id = credentialId,
                UserId = userId,
                Email = email,
                PasswordHash = passwordHash
            };

            dbCredential = new DbUserCredential()
            {
                Id = credentialId,
                UserId = userId,
                Email = email,
                PasswordHash = passwordHash
            };

            userAvatar = new UserAvatar
            {
                Id = userId,
                AvatarExtension = avatarExtension,
                Avatar = avatar,
                UserId = dbUser.Id
            };

            dbUserAvatar = new DbUsersAvatars
            {
                Id = userId,
                AvatarExtension = avatarExtension,
                Avatar = avatar,
                UserId = dbUser.Id
            };

            dbOptionsCreateUser = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_new_user_test")
                .Options;

            dbOptionsCreateCredential = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_new_credential_test")
                .Options;

            dbOptionsCreateAvatar = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_new_avatar_test")
                .Options;

            #endregion

            #region Delete user tests

            dbUserCredential = new DbUserCredential
            {
                Id = credentialId,
                UserId = userId,
                Email = "example@gmail.com",
                PasswordHash = "hashpassword",
                IsActive = true
            };

            #endregion

        }

        #region Create user tests

        [Test]
        public void CreateUserTest()
        {
            using var dbContext = new TPlatformDbContext(dbOptionsCreateUser);
            repository = new UserRepository(mapper, dbContext, logger.Object);
            repository.CreateUser(user, email);

            DbUserComparer comparer = new DbUserComparer();
            Assert.AreEqual(1, dbContext.Users.CountAsync().Result);
            Assert.IsTrue(comparer.Equals(
                dbUser,
                dbContext
                    .Users
                    .FirstOrDefaultAsync()
                    .Result));
        }

        [Test]
        public void CreateUserCredential()
        {
            using var dbContext = new TPlatformDbContext(dbOptionsCreateCredential);
            repository = new UserRepository(mapper, dbContext, logger.Object);
            repository.CreateUserCredential(credential);

            DbUserCredentialsComparer comparer = new DbUserCredentialsComparer();
            Assert.AreEqual(1, dbContext.UsersCredentials.CountAsync().Result);
            Assert.IsTrue(comparer.Equals(
                dbCredential,
                dbContext
                    .UsersCredentials
                    .FirstOrDefaultAsync()
                    .Result));
        }


        [Test]
        public void CreateUserAvatar()
        {
            using var dbContext = new TPlatformDbContext(dbOptionsCreateAvatar);
            repository = new UserRepository(mapper, dbContext, logger.Object);
            repository.CreateUser(user, email);
            repository.CreateUserAvatar(userAvatar);
            DbUserAvatarComparer comparer = new DbUserAvatarComparer();
            Assert.AreEqual(1, dbContext.UsersAvatars.CountAsync().Result);
            Assert.IsTrue(comparer.Equals(
                dbUserAvatar,
                dbContext
                    .UsersAvatars
                    .FirstOrDefaultAsync()
                    .Result));
        }

        #endregion

        #region Delete user tests

        [Test]
        public void DeleteUserOk()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserOkey")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            repository = new UserRepository(mapper, dbContext, logger.Object);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            repository.DeleteUser(userId);
            var expectedField = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId).IsActive;

            Assert.IsFalse(expectedField);
        }


        [Test]
        public void DeleteUserIsActiveFalse()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserIsActiveFalse")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            repository = new UserRepository(mapper, dbContext, logger.Object);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            dbUserCredential.IsActive = false;

            Assert.Throws<BadRequestException>(() => repository.DeleteUser(userId));

            // Come back
            dbUserCredential.IsActive = true;
        }

        [Test]
        public void DeleteNotExistUser()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteNotExistUser")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            repository = new UserRepository(mapper, dbContext, logger.Object);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            Guid temp = userId;
            userId = Guid.NewGuid();

            Assert.Throws<NotFoundException>(() => repository.DeleteUser(userId));
            // Come back
            userId = temp;
        }

        #endregion

        #region ConfirmTest

        [Test]
        public void ConfirmUserOk()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "ConfirmUserOkey")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            repository = new UserRepository(mapper, dbContext, logger.Object);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            dbUserCredential.IsActive = false;

            repository.ConfirmUser(dbUserCredential.Email);

            var expectedField = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == email).IsActive;

            Assert.IsTrue(expectedField);
        }

        [Test]
        public void ConfirmUserIsActiveTrue()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "ConfirmUserIsActiveTrue")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            repository = new UserRepository(mapper, dbContext, logger.Object);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            var exception = Assert.Throws< BadRequestException> (() => repository.ConfirmUser(email));


            Assert.AreEqual("User was confirmed early", exception.Message);
        }

        [Test]
        public void ConfirmUserNotFound()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "ConfirmUserNotFound")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            repository = new UserRepository(mapper, dbContext, logger.Object);


            var exception = Assert.Throws<NotFoundException>(() => repository.ConfirmUser(email));


            Assert.AreEqual("Not found User for confirm", exception.Message);
        }
        #endregion
    }
}