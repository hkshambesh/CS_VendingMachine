using System;

namespace CsVendingMachine.Types
{
    public class Product
    {
        public Guid Id { get; set; }
        public ProductType Type { get; set; }
        public int StockCount { get; set; }
        public decimal Price { get; set; }
    }
}