using System;
using CsVendingMachine.Types;

namespace CsVendingMachine.Services
{
    public interface IVendService
    {
        /// <summary>
        /// Processes the vend sale
        /// </summary>
        VendResult ProcessVend(Guid cardId, string cardPin, Guid productId);
    }
}