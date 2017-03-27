using System;
using System.Linq;

namespace CsVendingMachine.Services.Implementation
{
    public class AccountBalanceService : IAccountBalanceService
    {
        private readonly ITestDataRepositoryService _testDataRepositoryService;

        private readonly object _balanceLock = new object();

        public AccountBalanceService(ITestDataRepositoryService testDataRepositoryService)
        {
            _testDataRepositoryService = testDataRepositoryService;
        }

        /// <summary>
        /// Gets the balance of the card account
        /// </summary>
        public decimal GetBalance(Guid cardId)
        {
            var account = _testDataRepositoryService.GetCards().FirstOrDefault(x => x.Id == cardId)?.Account;

            return account?.Balance ?? 0;
        }

        /// <summary>
        /// Updates the balance of the card account
        /// </summary>
        public void UpdateBalance(Guid cardId, decimal amount)
        {
            var account = _testDataRepositoryService.GetCards().FirstOrDefault(x => x.Id == cardId)?.Account;

            if (account == null)
            {
                return;
            }

            // locking the update balance for multiple access at the same time
            lock (_balanceLock)
            {
                // decrement balance (Debit account)
                account.Balance -= amount;
            }
        }
    }
}