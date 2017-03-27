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
    public class ProductPriceServiceTests
    {
        private Fixture _fixture;
        private Guid _productId;

        private Mock<ITestDataRepositoryService> _iTestDataRepositoryService;

        private ProductPriceService _service;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();

            _productId = _fixture.Create<Guid>();

            _iTestDataRepositoryService = new Mock<ITestDataRepositoryService>();

            _service = new ProductPriceService(_iTestDataRepositoryService.Object);
        }

        [TestMethod]
        public void when_product_not_found_should_return_zero_as_default_price()
        {
            // act
            var products = new List<Product>();
            _iTestDataRepositoryService.Setup(x => x.GetProducts()).Returns(products);

            // actual
            var actual = _service.GetPrice(_productId);

            // assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void when_product_price_is_found_and_returned()
        {
            // act
            var price = 0.50m;

            var products = new List<Product>
            {
                new Product{ Id = _productId, Price = price}
            };
            _iTestDataRepositoryService.Setup(x => x.GetProducts()).Returns(products);

            // actual
            var actual = _service.GetPrice(_productId);

            // assert
            Assert.AreEqual(price, actual);
        }
    }
}