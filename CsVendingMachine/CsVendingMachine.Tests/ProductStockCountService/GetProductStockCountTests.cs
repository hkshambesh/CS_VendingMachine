using System;
using System.Collections.Generic;
using CsVendingMachine.Services;
using CsVendingMachine.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace CsVendingMachine.Tests.ProductStockCountService
{
    [TestClass]
    public class GetProductStockCountTests
    {
        private Fixture _fixture;
        private Guid _productId;

        private Mock<ITestDataRepositoryService> _iTestDataRepositoryService;

        private Services.Implementation.ProductStockCountService _service;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();

            _productId = _fixture.Create<Guid>();

            _iTestDataRepositoryService = new Mock<ITestDataRepositoryService>();

            _service = new Services.Implementation.ProductStockCountService(_iTestDataRepositoryService.Object);
        }

        [TestMethod]
        public void when_product_not_found_should_return_zero_as_default_stock_count()
        {
            // act
            var products = new List<Product>();
            _iTestDataRepositoryService.Setup(x => x.GetProducts()).Returns(products);

            // actual
            var actual = _service.GetProductStockCount(_productId);

            // assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void when_product_stock_count_is_found_and_returned()
        {
            // act
            var stockCount = 25;

            var products = new List<Product>
            {
                new Product{ Id = _productId, StockCount = stockCount}
            };

            _iTestDataRepositoryService.Setup(x => x.GetProducts()).Returns(products);

            // actual
            var actual = _service.GetProductStockCount(_productId);

            // assert
            Assert.AreEqual(stockCount, actual);
        }
    }
}