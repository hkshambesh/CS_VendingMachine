using System;
using System.Collections.Generic;
using System.Threading;
using CsVendingMachine.Services;
using CsVendingMachine.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace CsVendingMachine.Tests.ProductStockCountService
{
    [TestClass]
    public class UpdateProductStockCountTests
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
        public void when_product_stock_count_is_updated_multiple_times_at_the_same_time()
        {
            // act
            var stockCount = 25;

            var products = new List<Product>
            {
                new Product{ Id = _productId, StockCount = stockCount}
            };
            _iTestDataRepositoryService.Setup(x => x.GetProducts()).Returns(products);

            var t1 = new Thread(UpdateStockCountMultipleTimes);
            var t2 = new Thread(UpdateStockCountMultipleTimes);

            // actual
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            // assert
            Assert.AreEqual(15, _service.GetProductStockCount(_productId));
        }

        private void UpdateStockCountMultipleTimes()
        {
            for (int x = 0; x < 5; x++)
            {
                _service.UpdateProductStockCount(_productId);
            }
        }
    }
}