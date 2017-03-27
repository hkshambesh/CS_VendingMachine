using System.Collections.Generic;
using CsVendingMachine.Types;

namespace CsVendingMachine.Services
{
    /// <summary>
    /// Assuming this is the repository with data for testing purposes
    /// </summary>
    public interface ITestDataRepositoryService
    {
        /// <summary>
        /// Gets list of cards test data
        /// </summary>
        List<Card> GetCards();

        /// <summary>
        /// Gets list of products test data
        /// </summary>
        List<Product> GetProducts();

        /// <summary>
        /// Creates Test Data
        /// </summary>
        void CreateTestData();
    }
}