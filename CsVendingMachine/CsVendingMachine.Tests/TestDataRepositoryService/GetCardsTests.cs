using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsVendingMachine.Tests.TestDataRepositoryService
{
    [TestClass]
    public class GetCardsTests
    {
        [TestMethod]
        public void when_no_cards_found_should_return_empty_list()
        {
            // act
            var service = new Services.Implementation.TestDataRepositoryService();

            // actual
            var actual = service.GetCards();

            // assert
            Assert.IsTrue(actual.Count == 0);
        }

        [TestMethod]
        public void when_cards_found_should_return_list_of_cards()
        {
            // act
            var service = new Services.Implementation.TestDataRepositoryService();
            service.CreateTestData();

            // actual
            var actual = service.GetCards();

            // assert
            Assert.IsTrue(actual.Count > 0);
        }
    }
}