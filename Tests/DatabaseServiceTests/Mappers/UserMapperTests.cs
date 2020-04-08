using DataBaseService.Mappers.Interfaces;
using System;

using NUnit.Framework;

using DTO;
using DataBaseService.Mappers;
using DataBaseService.Database.Models;
using DatabaseServiceTests.Comparators;

namespace DatabaseServiceTests.Mappers
{
    public class UserMapperTests
    {
        #region BIO
        private Guid userId = Guid.NewGuid();
        private Guid credentialId = Guid.NewGuid();

        private string firstName = "Adam";
        private string lastName = "Yablokov";
        private string email = "adam.ya@eden.org";
        private DateTime birth = DateTime.MinValue;
        private string passwordHash = "passwordHash";
        #endregion

        IUserMapper mapper;

        private User user;
        private DbUser dbUser;
        private UserCredential credential;
        private DbUserCredential dbCredential;

        private UserComparer userComparer = new UserComparer();
        private DbUserComparer dbUserComparer = new DbUserComparer();
        private UserCredentialsComparer userCredentialsComparer = new UserCredentialsComparer();
        private DbUserCredentialsComparer dbUserCredentialsComparer = new DbUserCredentialsComparer();

        [OneTimeSetUp]
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
            Assert.IsTrue(dbUserComparer.Equals(dbUser, mapper.MapToDbUser(user)));
        }

        [Test]
        public void MapDbUser()
        {
            Assert.IsTrue(userComparer.Equals(user, mapper.MapUser(dbUser, email)));
        }

        [Test]
        public void MapCredential()
        {
            Assert.IsTrue(dbUserCredentialsComparer.Equals(dbCredential, mapper.MapToDbUserCredential(credential)));
        }

        [Test]
        public void MapDbCredential()
        {
            Assert.IsTrue(userCredentialsComparer.Equals(credential, mapper.MapUserCredential(dbCredential)));
        }

    }
}
