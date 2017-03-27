using System;
using CsVendingMachine.Types;

namespace CsVendingMachine.Services.Implementation
{
    public class VendService : IVendService
    {
        private readonly IProductStockCountService _productStockCountService;
        private readonly ICardPinService _cardPinService;
        private readonly IAccountBalanceService _accountBalanceService;
        private readonly IProductPriceService _productPriceService;

        public VendService(IProductStockCountService productStockCountService, 
            ICardPinService cardPinService, 
            IAccountBalanceService accountBalanceService, 
            IProductPriceService productPriceService)
        {
            _productStockCountService = productStockCountService;
            _cardPinService = cardPinService;
            _accountBalanceService = accountBalanceService;
            _productPriceService = productPriceService;
        }

        /// <summary>
        /// Processes the vend sale
        /// </summary>
        public VendResult ProcessVend(Guid cardId, string cardPin, Guid productId)
        {
            var result = new VendResult();

            // check product stock count if available
            int stockCount = _productStockCountService.GetProductStockCount(productId);

            if (stockCount == 0)
            {
                result.ErrorDescription = $"Product out of stock. productId:{productId}";
                return result;
            }

            // check card pin is valid
            bool cardPinValid = _cardPinService.ValidCardPin(cardId, cardPin);

            if (!cardPinValid)
            {
                result.ErrorDescription = $"Invalid card PIN. cardId:{cardId}";
                return result;
            }

            // get product price
            decimal price = _productPriceService.GetPrice(productId);

            // check card account balance
            decimal balance = _accountBalanceService.GetBalance(cardId);

            // assuming product price (50p)
            // checks if card balance is less then the product price
            if (balance < price)
            {
                result.ErrorDescription = $"Insufficient funds. cardId:{cardId}";
                return result;
            }

            // update card account balance with purchase
            _accountBalanceService.UpdateBalance(cardId, price);

            // update product stock count
            _productStockCountService.UpdateProductStockCount(productId);

            // ASSUMPTION make purchase and create successful transaction
            result.IsSuccess = true;

            return result;
        }
    }
}