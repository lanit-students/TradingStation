﻿using System;
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

namespace DatabaseServiceTests.Repositories
{
    public class UserRepositoryDeleteTests
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

        private string firstName = "Adam";
        private string lastName = "Yablokov";
        private string email = "adam.ya@eden.org";
        private DateTime birth = DateTime.MinValue;
        private string passwordHash = "passwordHash";

        private User user;
        private DbUser dbUser;
        private UserCredential credential;
        private DbUserCredential dbCredential;
        #endregion

        #region Delete user tests
        private DbUserCredential dbUserCredential;
        #endregion

        [OneTimeSetUp]
        public void Initialize()
        {
            #region Common
            mapper = new UserMapper();
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

            dbOptionsCreateUser = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_new_user_test")
                .Options;

            dbOptionsCreateCredential = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_new_credential_test")
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
            repository = new UserRepository(mapper, dbContext);
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
            repository = new UserRepository(mapper, dbContext);
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
        #endregion

        #region Delete user tests
        [Test]
        public void DeleteUserOk()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserOkey")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            repository = new UserRepository(mapper, dbContext);

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
            repository = new UserRepository(mapper, dbContext);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            dbUserCredential.IsActive = false;

            var exception = Assert.Throws<BadRequestException>(() => repository.DeleteUser(userId));

            Assert.AreEqual("User was deleted early or not confirmed", exception.Message);
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
            repository = new UserRepository(mapper, dbContext);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            Guid temp = userId;
            userId = Guid.NewGuid();

            var exception = Assert.Throws<NotFoundException>(() => repository.DeleteUser(userId));

            Assert.AreEqual("Not found User for delete", exception.Message);
            // Come back
            userId = temp;
        }
        #endregion
    }
}
