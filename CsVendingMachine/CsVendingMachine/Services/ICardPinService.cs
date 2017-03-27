using System;

namespace CsVendingMachine.Services
{
    public interface ICardPinService
    {
        /// <summary>
        /// Checks if the pin is correct
        /// </summary>
        bool ValidCardPin(Guid cardId, string cardPin);
    }
}