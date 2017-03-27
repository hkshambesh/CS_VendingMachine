using System;

namespace CsVendingMachine.Services
{
    public interface IProductPriceService
    {
        /// <summary>
        /// Gets the price of the product
        /// </summary>
        decimal GetPrice(Guid productId);
    }
}