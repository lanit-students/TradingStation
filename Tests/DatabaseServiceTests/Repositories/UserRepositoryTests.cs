using DataBaseService.Database;
using DataBaseService.Database.Models;
using DataBaseService.Mappers;
using DataBaseService.Repositories;
using Kernel.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace DatabaseServiceTests.Repositories
{
    public class UserRepositoryDeleteTests
    {
        private UserRepository userRepository;
        private DbUserCredential dbUserCredential;
        private UserMapper mapper;
        private Guid userId;
        private Guid id;

        [SetUp]
        public void Initialize()
        {
            mapper = new UserMapper();
            userId = Guid.NewGuid();
            id = Guid.NewGuid();

            dbUserCredential = new DbUserCredential
            {
                Id = id,
                UserId = userId,
                Email = "example@gmail.com",
                PasswordHash = "hashpassword",
                IsActive = true
            };
        }

        [Test]
        public void DeleteUserOk()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserOkey")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            userRepository = new UserRepository(mapper, dbContext);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            userRepository.DeleteUser(userId);
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
            userRepository = new UserRepository(mapper, dbContext);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            dbUserCredential.IsActive = false;

            var exception = Assert.Throws<BadRequestException>(() => userRepository.DeleteUser(userId));

            Assert.AreEqual("User was deleted early or not confirmed", exception.Message);
        }

        [Test]
        public void DeleteNotExistUser()
        {
            var options = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteNotExistUser")
                .Options;

            using var dbContext = new TPlatformDbContext(options);
            userRepository = new UserRepository(mapper, dbContext);

            dbContext.UsersCredentials.Add(dbUserCredential);
            dbContext.SaveChanges();

            userId = Guid.NewGuid();

            var exception = Assert.Throws<NotFoundException>(() => userRepository.DeleteUser(userId));

            Assert.AreEqual("Not found User for delete", exception.Message);
        }
    }
}
