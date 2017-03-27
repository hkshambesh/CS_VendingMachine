using System;

namespace CsVendingMachine.Services
{
    public interface IAccountBalanceService
    {
        /// <summary>
        /// Gets the balance of the card account
        /// </summary>
        decimal GetBalance(Guid cardId);

        /// <summary>
        /// Updates the balance of the card account
        /// </summary>
        void UpdateBalance(Guid cardId, decimal amount);
    }
}