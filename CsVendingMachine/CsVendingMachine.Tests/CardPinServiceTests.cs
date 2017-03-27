using System;
using System.Collections.Generic;
using CsVendingMachine.Services;
using CsVendingMachine.Services.Implementation;
using CsVendingMachine.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace CsVendingMachine.Tests
{
    [TestClass]
    public class CardPinServiceTests
    {
        private Fixture _fixture;

        private Guid _cardId;
        private string _cardPin;

        private Mock<ITestDataRepositoryService> _iTestDataRepositoryService;

        private CardPinService _service;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _cardId = _fixture.Create<Guid>();
            _cardPin = _fixture.Create<string>();

            _iTestDataRepositoryService = new Mock<ITestDataRepositoryService>();

            _service = new CardPinService(_iTestDataRepositoryService.Object);
        }

        [TestMethod]
        public void when_card_pin_is_invalid()
        {
            // act
            var cards = new List<Card>
            {
                new Card{ Pin = "3333", Id = _cardId}
            };

            _iTestDataRepositoryService.Setup(x => x.GetCards()).Returns(cards);

            // actual
            var actual = _service.ValidCardPin(_cardId, _cardPin);

            // assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void when_card_pin_is_valid()
        {
            // act
            var cards = new List<Card>
            {
                new Card{ Pin = _cardPin, Id = _cardId}
            };

            _iTestDataRepositoryService.Setup(x => x.GetCards()).Returns(cards);

            // actual
            var actual = _service.ValidCardPin(_cardId, _cardPin);

            // assert
            Assert.IsTrue(actual);
        }
    }
}