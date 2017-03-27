using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsVendingMachine.Tests.TestDataRepositoryService
{
    [TestClass]
    public class GetProductsTests
    {
        [TestMethod]
        public void when_no_products_found_should_return_empty_list()
        {
            // act
            var service = new Services.Implementation.TestDataRepositoryService();

            // actual
            var actual = service.GetProducts();

            // assert
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void when_products_found_should_return_list_of_products()
        {
            // act
            var service = new Services.Implementation.TestDataRepositoryService();
            service.CreateTestData();

            // actual
            var actual = service.GetProducts();

            // assert
            Assert.IsTrue(actual.Count > 0);
        }
    }
}