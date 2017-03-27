using System;
using System.Collections.Generic;
using CsVendingMachine.Services;
using CsVendingMachine.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace CsVendingMachine.Tests.AccountBalanceService
{
    [TestClass]
    public class GetBalanceTests
    {
        private Fixture _fixture;
        private Guid _cardId;

        private Mock<ITestDataRepositoryService> _iTestDataRepositoryService;

        private Services.Implementation.AccountBalanceService _service;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();

            _cardId = _fixture.Create<Guid>();

            _iTestDataRepositoryService = new Mock<ITestDataRepositoryService>();

            _service = new Services.Implementation.AccountBalanceService(_iTestDataRepositoryService.Object);
        }

        [TestMethod]
        public void when_card_not_found_should_return_zero_as_default_balance()
        {
            // act
            var cards = new List<Card>();
            _iTestDataRepositoryService.Setup(x => x.GetCards()).Returns(cards);

            // actual
            var actual = _service.GetBalance(_cardId);

            // assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void when_card_is_found_and_should_return_account_balance_linked_to_card()
        {
            // act
            var balance = 10m;

            var cards = new List<Card>
            {
                new Card{ Id = _cardId, Account = new Account
                {
                    Balance = balance
                }}
            };

            _iTestDataRepositoryService.Setup(x => x.GetCards()).Returns(cards);

            // actual
            var actual = _service.GetBalance(_cardId);

            // assert
            Assert.AreEqual(balance, actual);
        }
    }
}