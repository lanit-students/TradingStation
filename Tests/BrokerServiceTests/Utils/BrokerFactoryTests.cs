using BrokerServices.Utils;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using BrokerService.Controllers;
using BrokerServices;
using TinkoffIntegrationLib;
using Kernel.CustomExceptions;
using DTO;

namespace BrokerServiceTests.Utils
{
    public class BrokerFactoryTests
    {
        private Mock<ILogger<BrokerController>> loggerMock;
        private BrokerData brokerData;

        [SetUp]
        public void Initialization()
        {
            loggerMock = new Mock<ILogger<BrokerController>>();
            brokerData = new BrokerData();
        }

        [Test]
        public void BrokerFactoryTinkoffBankBrokerCorrectBankType()
        {
            Assert.IsTrue(BrokerFactory.Create(BankType.TinkoffBank, loggerMock.Object,brokerData) is TinkoffBankBroker);
        }

        [Test]
        public void BrokerFactoryBadRequestExceptionIncorrectBankType()
        {
            BankType incorrectBankType = (BankType)2;

            Assert.Throws<BadRequestException>(() => BrokerFactory.Create(incorrectBankType, loggerMock.Object,brokerData));
        }
    }
}
