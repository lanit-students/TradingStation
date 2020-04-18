using Interfaces;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using BrokerService.Commands;
using BrokerService.Interfaces;
using BrokerService;
using Kernel.CustomExceptions;

namespace BrokerServiceTests.Commands
{
    public class GetImarketInstrumentCommandTests
    {
        private Mock<IBroker> brokerMock;
        private Mock<IMarketInstrument> marketInstrumentMock;
        private IGetImarketInstrumentCommand command;
        private string Id;

        [SetUp]
        public void Initialization()
        {
            marketInstrumentMock = new Mock<IMarketInstrument>();

            brokerMock = new Mock<IBroker>();
            brokerMock
                .Setup(x => x.GetAllBonds())
                .Returns(new List<IMarketInstrument>());

            brokerMock
                .Setup(x => x.GetAllCurrencies())
                .Returns(new List<IMarketInstrument>());

            brokerMock
                .Setup(x => x.GetAllStocks())
                .Returns(new List<IMarketInstrument>());

            brokerMock
                .Setup(x => x.GetBond(It.IsAny<string>()))
                .Returns(marketInstrumentMock.Object);

            brokerMock
                .Setup(x => x.GetCurrency(It.IsAny<string>()))
                .Returns(marketInstrumentMock.Object);

            brokerMock
                .Setup(x => x.GetStock(It.IsAny<string>()))
                .Returns(marketInstrumentMock.Object);

            command = new GetImarketInstrumentCommand();

            Id = "Not empty string";
        }


        [TestCase(CommandsType.GetAllBonds)]
        [TestCase(CommandsType.GetAllCurrencies)]
        [TestCase(CommandsType.GetAllStocks)]
        [TestCase(CommandsType.GetBond)]
        [TestCase(CommandsType.GetCurrency)]
        [TestCase(CommandsType.GetStock)]
        public void GetImarketInstrumentCommandImarketInstrumentCorrectCommandsType(CommandsType commandType)
        {
            if (commandType == CommandsType.GetAllBonds || commandType == CommandsType.GetAllCurrencies || commandType == CommandsType.GetAllStocks)
            {
                Assert.IsTrue(command.Execute(commandType, brokerMock.Object) is List<IMarketInstrument>);
            }
            else
            {
                Assert.IsTrue(command.Execute(commandType, brokerMock.Object, Id) is IMarketInstrument);
            }
        }

        [Test]
        public void GetImarketInstrumentCommandBadRequestExceptionIncorrectCommandsType()
        {
            CommandsType commandType = (CommandsType)10;

            Assert.Throws<BadRequestException>(() => command.Execute(commandType, brokerMock.Object));
        }
    }
}
