using System;
using Interfaces;
using NewsService.Enums;
using NewsService.Utils;
using NUnit.Framework;
using RssIntegrationLib;

namespace NewsServiceTests.Utils
{
    public class NewsPublisherFactoryTests
    {
        private NewsPublisherTypes ramblerType = NewsPublisherTypes.Rambler;
        private INewsPublisher ramblerNewsPublisher;

        [SetUp]
        public void Initialize()
        {
            ramblerNewsPublisher = new RamblerRssReader();
        }

        [Test]
        public void CreateReturnsNotNullNewsPublisher()
        {
            foreach (NewsPublisherTypes type in Enum.GetValues(typeof(NewsPublisherTypes)))
            {
                Assert.NotNull(NewsPublisherFactory.Create(type));
            }
        }

        [Test]
        public void CreateRamblerNewsPublisherResultHasCorrectType()
        {
            var typeOfCreatedObject = NewsPublisherFactory.Create(ramblerType).GetType();

            Assert.AreEqual(typeOfCreatedObject, ramblerNewsPublisher.GetType());
        }
    }
}
