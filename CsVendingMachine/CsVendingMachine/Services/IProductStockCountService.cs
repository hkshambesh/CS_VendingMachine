using System;

namespace CsVendingMachine.Services
{
    public interface IProductStockCountService
    {
        /// <summary>
        /// Gets product stock count
        /// </summary>
        int GetProductStockCount(Guid productId);

        /// <summary>
        /// Updates product stock count
        /// </summary>
        void UpdateProductStockCount(Guid productId);
    }
}