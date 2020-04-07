using DataBaseService.Mappers.Interfaces;
using System;

using NUnit.Framework;

using DTO;
using DataBaseService.Mappers;
using DataBaseService.Database.Models;

namespace DatabaseServiceTests.UserLogic.Mappers
{
    public class UserMapperTests
    {
        #region BIO
        Guid userId = Guid.NewGuid();
        Guid credentialId = Guid.NewGuid();

        string firstName = "Adam";
        string lastName = "Yablokov";
        string email = "adam.ya@eden.org";
        DateTime birth = DateTime.MinValue;
        string passwordHash = "passwordHash";
        #endregion

        IUserMapper mapper;

        User user;
        DbUser dbUser;

        UserCredential credential;
        DbUserCredential dbCredential;

        [SetUp]
        public void Initialize()
        {
            mapper = new UserMapper();

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
        }

        [Test]
        public void MapUser()
        {
            Assert.AreEqual(
                dbUser,
                mapper.MapToDbUser(user));
        }

        [Test]
        public void MapDbUser()
        {
            Assert.AreEqual(
                user,
                mapper.MapUser(dbUser, email));
        }

        [Test]
        public void MapCredential()
        {
            Assert.AreEqual(
                dbCredential,
                mapper.MapToDbUserCredential(credential));
        }

        [Test]
        public void MapDbCredential()
        {
            Assert.AreEqual(
                credential,
                mapper.MapUserCredential(dbCredential));
        }

    }
}
