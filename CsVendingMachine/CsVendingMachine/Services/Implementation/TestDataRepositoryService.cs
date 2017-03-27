using System;
using System.Collections.Generic;
using CsVendingMachine.Types;

namespace CsVendingMachine.Services.Implementation
{
    /// <summary>
    /// Assuming this is the repository with data for cards and products 
    /// Holds data in memory
    /// </summary>
    public sealed class TestDataRepositoryService : ITestDataRepositoryService
    {
        private List<Card> _cards;
        private List<Product> _products;

        public TestDataRepositoryService()
        {
            _cards = new List<Card>();
            _products = new List<Product>();
        }

        /// <summary>
        /// Gets list of cards test data
        /// </summary>
        public List<Card> GetCards()
        {
            return _cards;
        }

        /// <summary>
        /// Gets list of products test data
        /// </summary>
        public List<Product> GetProducts()
        {
            return _products;
        }

        /// <summary>
        /// Creates Test Data
        /// Tests not required as this is used for testing purposes
        /// </summary>
        public void CreateTestData()
        {
            // add SoftDrink product
            _products.Add(new Product
            {
                Id = Guid.NewGuid(),
                StockCount = 25,
                Type = ProductType.SoftDrink,
                Price = 0.50m
            });

            // add account
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Cash Account",
                Balance = 5m // £5
            };

            // add card 1
            var card1 = new Card
            {
                Id = Guid.NewGuid(),
                Name = "Card1",
                Pin = "1111",
                Type = CardType.Cash,
                Account = account // link to same account
            };

            // add card 2
            var card2 = new Card
            {
                Id = Guid.NewGuid(),
                Name = "Card2",
                Pin = "2222",
                Type = CardType.Cash,
                Account = account // link to same account
            };

            _cards.Add(card1);
            _cards.Add(card2);
        }
    }
}