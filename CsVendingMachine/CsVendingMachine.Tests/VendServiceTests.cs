using System;
using CsVendingMachine.Services;
using CsVendingMachine.Services.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace CsVendingMachine.Tests
{
    [TestClass]
    public class VendServiceTests
    {
        private Fixture _fixture;
        private Guid _cardId;
        private string _cardPin;
        private Guid _productId;

        private VendService _service;

        private Mock<IProductStockCountService> _iProductStockCountService;
        private Mock<ICardPinService> _iCardPinService;
        private Mock<IAccountBalanceService> _iAccountBalanceService;
        private Mock<IProductPriceService> _iProductPriceService;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();

            _cardId = _fixture.Create<Guid>();
            _cardPin = _fixture.Create<string>();
            _productId = _fixture.Create<Guid>();

            _iProductStockCountService = new Mock<IProductStockCountService>();
            _iCardPinService = new Mock<ICardPinService>();
            _iAccountBalanceService = new Mock<IAccountBalanceService>();
            _iProductPriceService = new Mock<IProductPriceService>();

            _service = new VendService(_iProductStockCountService.Object, _iCardPinService.Object, _iAccountBalanceService.Object, _iProductPriceService.Object);
        }

        [TestMethod]
        public void when_product_stock_count_is_zero()
        {
            // act
            _iProductStockCountService.Setup(x => x.GetProductStockCount(_productId)).Returns(0);

            // actual
            var actual = _service.ProcessVend(_cardId, _cardPin, _productId);

            // assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.AreEqual($"Product out of stock. productId:{_productId}", actual.ErrorDescription);
        }

        [TestMethod]
        public void when_card_pin_is_invalid()
        {
            // act
            _iProductStockCountService.Setup(x => x.GetProductStockCount(_productId)).Returns(25);
            _iCardPinService.Setup(x => x.ValidCardPin(_cardId, _cardPin)).Returns(false);

            // actual
            var actual = _service.ProcessVend(_cardId, _cardPin, _productId);

            // assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.AreEqual($"Invalid card PIN. cardId:{_cardId}", actual.ErrorDescription);
        }

        [TestMethod]
        public void when_card_account_balance_is_less_than_product_price()
        {
            // act
            _iProductStockCountService.Setup(x => x.GetProductStockCount(_productId)).Returns(25);
            _iCardPinService.Setup(x => x.ValidCardPin(_cardId, _cardPin)).Returns(true);
            _iProductPriceService.Setup(x => x.GetPrice(_productId)).Returns(0.50m);
            _iAccountBalanceService.Setup(x => x.GetBalance(_cardId)).Returns(0.20m);

            // actual
            var actual = _service.ProcessVend(_cardId, _cardPin, _productId);

            // assert
            Assert.IsFalse(actual.IsSuccess);
            Assert.AreEqual($"Insufficient funds. cardId:{_cardId}", actual.ErrorDescription);
        }

        [TestMethod]
        public void when_payment_is_successful_should_update_card_account_balance()
        {
            // act
            const decimal amount = 0.50m;

            _iProductStockCountService.Setup(x => x.GetProductStockCount(_productId)).Returns(25);
            _iCardPinService.Setup(x => x.ValidCardPin(_cardId, _cardPin)).Returns(true);
            _iProductPriceService.Setup(x => x.GetPrice(_productId)).Returns(amount);
            _iAccountBalanceService.Setup(x => x.GetBalance(_cardId)).Returns(1.00m);

            // actual
            var actual = _service.ProcessVend(_cardId, _cardPin, _productId);

            // assert
            Assert.IsTrue(actual.IsSuccess);
            _iAccountBalanceService.Verify(x=>x.UpdateBalance(_cardId, amount), Times.Once);
        }

        [TestMethod]
        public void when_payment_is_successful_should_update_product_stock_count()
        {
            // act
            const decimal amount = 0.50m;

            _iProductStockCountService.Setup(x => x.GetProductStockCount(_productId)).Returns(25);
            _iCardPinService.Setup(x => x.ValidCardPin(_cardId, _cardPin)).Returns(true);
            _iProductPriceService.Setup(x => x.GetPrice(_productId)).Returns(amount);
            _iAccountBalanceService.Setup(x => x.GetBalance(_cardId)).Returns(1.00m);

            // actual
            var actual = _service.ProcessVend(_cardId, _cardPin, _productId);

            // assert
            Assert.IsTrue(actual.IsSuccess);
            _iProductStockCountService.Verify(x=>x.UpdateProductStockCount(_productId), Times.Once);
        }
    }
}