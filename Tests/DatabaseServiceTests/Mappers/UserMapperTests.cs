using DataBaseService.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using DataBaseService.Mappers;

namespace DatabaseServiceTests.UserLogic.Mappers
{
    class UserMapperTests
    {
        IUserMapper mapper;

        [SetUp]
        public void Initialize()
        {
            mapper = new UserMapper();
        }

        [Test]
        public void Some()
        {

        }

    }
}
