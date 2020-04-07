using NUnit.Framework;
using Interfaces;
using BrokerService.Controllers;
using System;
using BrokerService;
using System.Collections.Generic;

namespace BrokerServiceTests
{
    [TestFixture]
    public class BrokerControllerTests
    {
        private BrokerController brokerController;
        [SetUp]
        public void Setup()
        {
             brokerController = new BrokerController(); 
        }

        [Test]
        public void GetCurrency_ReturnAggregateException()
        {
            Assert.Throws<AggregateException>(() => brokerController.GetCurrency(BrokerServices.BankType.TinkoffBank, "wrong string"));
        }
        [Test]
        public void GetAllCurrencies_ReturnAggregateException()
        {
            Assert.Throws<AggregateException>(() => brokerController.GetAllCurrencies(BrokerServices.BankType.TinkoffBank));
        }
        [Test]
        public void GetAllStocks_ReturnAggregateException()
        {
            Assert.Throws<AggregateException>(() => brokerController.GetAllStocks(BrokerServices.BankType.TinkoffBank));
        }
        [Test]
        public void GetStock_ReturnAggregateException()
        {
            Assert.Throws<AggregateException>(() => brokerController.GetStock(BrokerServices.BankType.TinkoffBank,"wrong string"));
        }
        [Test]
        public void GetAllBonds_ReturnAggregateException()
        {
            Assert.Throws<AggregateException>(() => brokerController.GetAllBonds(BrokerServices.BankType.TinkoffBank));
        }
        [Test]
        public void GetBond_ReturnAggregateException()
        {
            Assert.Throws<AggregateException>(() => brokerController.GetBond(BrokerServices.BankType.TinkoffBank, "wrong string"));
        }
        [Test]
        public void GetCurrency_ReturnIMarketInstrument()
        {
            Assert.That(brokerController.GetBond(BrokerServices.BankType.TinkoffBank, Constants.Token) is IMarketInstrument);
        }
        [Test]
        public void GetAllCurrencies_ReturnListIMarketInstrument()
        {
            Assert.That(brokerController.GetBond(BrokerServices.BankType.TinkoffBank, Constants.Token) is List<IMarketInstrument>);
        }
        [Test]
        public void GetAllStocks_ReturnListIMarketInstrument()
        {
            Assert.That(brokerController.GetBond(BrokerServices.BankType.TinkoffBank, Constants.Token) is List<IMarketInstrument>);
        }
        [Test]
        public void GetStock_ReturnIMarketInstrument()
        {
            Assert.That(brokerController.GetBond(BrokerServices.BankType.TinkoffBank, Constants.Token) is IMarketInstrument);
        }
        [Test]
        public void GetAllBonds_ReturnListIMarketInstrument()
        {
            Assert.That(brokerController.GetBond(BrokerServices.BankType.TinkoffBank, Constants.Token) is List<IMarketInstrument>);
        }
        [Test]
        public void GetBond_ReturnIMarketInstrument()
        {
            Assert.That(brokerController.GetBond(BrokerServices.BankType.TinkoffBank, Constants.Token) is IMarketInstrument);
        }
    }
}