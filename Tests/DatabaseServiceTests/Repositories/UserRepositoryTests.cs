using System;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;
using Moq;

using DTO;
using DataBaseService.Database;
using DataBaseService.Database.Models;
using DataBaseService.Repositories;
using DataBaseService.Repositories.Interfaces;
using DataBaseService.Mappers.Interfaces;


namespace DatabaseServiceTests.Repositories
{
    public class UserRepositoryTests
    {
        IUserRepository repository;
        Mock<IUserMapper> mapper;
        DbContextOptions<TPlatformDbContext> dbOptions;

        #region BIO
        Guid userId = Guid.NewGuid();
        Guid credentialId = Guid.NewGuid();

        string firstName = "Adam";
        string lastName = "Yablokov";
        string email = "adam.ya@eden.org";
        DateTime birth = DateTime.MinValue;
        string passwordHash = "passwordHash";

        User user;
        DbUser dbUser;
        UserCredential credential;
        DbUserCredential dbCredential;
        #endregion

        [OneTimeSetUp]
        public void Initialize()
        {
            dbOptions = new DbContextOptionsBuilder<TPlatformDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_new_user_test")
                .Options;

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

            mapper = new Mock<IUserMapper>();

            mapper
                .Setup(m => m.MapToDbUserCredential(credential))
                .Returns(dbCredential);

            mapper
                .Setup(m => m.MapToDbUser(user))
                .Returns(dbUser);
        }

        [Test]
        public void CreateUserTest()
        {
            using var dbContext = new TPlatformDbContext(dbOptions);
            repository = new UserRepository(mapper.Object, dbContext);
            repository.CreateUser(user, email);

            Assert.AreEqual(1, dbContext.Users.CountAsync().Result);
            Assert.AreEqual(
                dbUser,
                dbContext
                .Users
                .FirstOrDefaultAsync()
                .Result);
        }

        public void CreateUserCredential()
        {
            using var dbContext = new TPlatformDbContext(dbOptions);
            repository = new UserRepository(mapper.Object, dbContext);
            repository.CreateUserCredential(credential);

            Assert.AreEqual(1, dbContext.Users.CountAsync().Result);
            Assert.AreEqual(
                dbCredential,
                dbContext
                .UsersCredentials
                .FirstOrDefaultAsync()
                .Result);
        }



    }
}
