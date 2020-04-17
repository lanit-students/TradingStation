using BrokerServices.Utils;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using BrokerService.Controllers;
using BrokerServices;
using TinkoffIntegrationLib;
using Kernel.CustomExceptions;

namespace BrokerServiceTests.Utils
{
    public class BrokerFactoryTests
    {
        private Mock<ILogger<BrokerController>> loggerMock;

        [SetUp]
        public void Initialization()
        {
            loggerMock = new Mock<ILogger<BrokerController>>();
        }

        [Test]
        public void BrokerFactoryTinkoffBankBrokerCorrectBankType()
        {
            Assert.IsTrue(BrokerFactory.Create(BankType.TinkoffBank, loggerMock.Object) is TinkoffBankBroker);
        }

        [Test]
        public void BrokerFactoryBadRequestExceptionIncorrectBankType()
        {
            BankType incorrectBankType = (BankType)2;

            Assert.Throws<BadRequestException>(() => BrokerFactory.Create(incorrectBankType, loggerMock.Object));
        }
    }
}
