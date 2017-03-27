using System;
using System.Collections.Generic;
using System.Threading;
using CsVendingMachine.Services;
using CsVendingMachine.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace CsVendingMachine.Tests.AccountBalanceService
{
    [TestClass]
    public class UpdateBalanceTests
    {
        private Fixture _fixture;
        private Guid _cardId;
        private decimal _amount;

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
        public void when_account_balance_is_updated_multiple_times_at_the_same_time()
        {
            // act
            _amount = 1m; // £1
            var cards = new List<Card>
            {
                new Card{ Id = _cardId, Account = new Account
                {
                    Balance = 50m // £50
                }}
            };

            _iTestDataRepositoryService.Setup(x => x.GetCards()).Returns(cards);

            var t1 = new Thread(UpdateBalanceMultipleTimes);
            var t2 = new Thread(UpdateBalanceMultipleTimes);

            // actual
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            // assert
            Assert.AreEqual(30, _service.GetBalance(_cardId));
        }

        private void UpdateBalanceMultipleTimes()
        {
            for (int x = 0; x < 10; x++)
            {
                _service.UpdateBalance(_cardId, _amount);
            }
        }
    }
}