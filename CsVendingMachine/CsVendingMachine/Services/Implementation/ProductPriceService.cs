using System;
using System.Linq;

namespace CsVendingMachine.Services.Implementation
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly ITestDataRepositoryService _testDataRepositoryService;

        public ProductPriceService(ITestDataRepositoryService testDataRepositoryService)
        {
            _testDataRepositoryService = testDataRepositoryService;
        }

        /// <summary>
        /// Gets the price of the product
        /// </summary>
        public decimal GetPrice(Guid productId)
        {
            var product = _testDataRepositoryService.GetProducts().FirstOrDefault(x => x.Id == productId);

            return product?.Price ?? 0;
        }
    }
}