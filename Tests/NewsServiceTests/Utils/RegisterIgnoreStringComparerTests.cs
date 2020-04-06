using System.Collections.Generic;

using NUnit.Framework;

using NewsService.Utils;

namespace NewsServiceTests.Utils
{
    public class RegisterIgnoreStringComparerTests
    {
        private IEqualityComparer<string> comparer;

        [SetUp]
        public void Initialize()
        {
            comparer = new RegisterIgnoreStringComparer();
        }

        [Test]
        public void AllStringsNull()
        {
            Assert.AreEqual(true, comparer.Equals(null, null));
        }

        [Test]
        public void OneStringsNull()
        {
            Assert.AreEqual(false, comparer.Equals(null, "string"));
        }

        [Test]
        public void DifferentStrngs()
        {
            Assert.AreEqual(false, comparer.Equals("string1", "string2"));
        }

        [Test]
        public void EqualStrings()
        {
            Assert.AreEqual(true, comparer.Equals("string", "string"));
        }

        [Test]
        public void WithDifferentRegisterStrings()
        {
            Assert.AreEqual(true, comparer.Equals("sTrInG", "StRiNg"));
        }
    }
}
