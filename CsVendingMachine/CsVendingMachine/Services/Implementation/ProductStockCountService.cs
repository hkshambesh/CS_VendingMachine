using System;
using System.Linq;

namespace CsVendingMachine.Services.Implementation
{
    public class ProductStockCountService : IProductStockCountService
    {
        private readonly ITestDataRepositoryService _testDataRepositoryService;

        private readonly object _stockCountLock = new object();

        public ProductStockCountService(ITestDataRepositoryService testDataRepositoryService)
        {
            _testDataRepositoryService = testDataRepositoryService;
        }

        /// <summary>
        /// Gets product stock count
        /// </summary>
        public int GetProductStockCount(Guid productId)
        {
            var product = _testDataRepositoryService.GetProducts().FirstOrDefault(x => x.Id == productId);

            return product?.StockCount ?? 0;
        }

        /// <summary>
        /// Updates product stock count
        /// </summary>
        public void UpdateProductStockCount(Guid productId)
        {
            var product = _testDataRepositoryService.GetProducts().FirstOrDefault(x => x.Id == productId);

            // locking the update stock count update for multiple access at the same time
            lock (_stockCountLock)
            {
                if (product == null)
                {
                    return;
                }

                // decrement stock count
                product.StockCount -= 1;
            }
        }
    }
}