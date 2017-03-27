using System;
using System.Linq;

namespace CsVendingMachine.Services.Implementation
{
    public class CardPinService : ICardPinService
    {
        private readonly ITestDataRepositoryService _testDataRepositoryService;

        public CardPinService(ITestDataRepositoryService testDataRepositoryService)
        {
            _testDataRepositoryService = testDataRepositoryService;
        }

        /// <summary>
        /// Checks if the pin is correct
        /// </summary>
        public bool ValidCardPin(Guid cardId, string cardPin)
        {
            var cardFound = _testDataRepositoryService.GetCards().FirstOrDefault(x=>x.Id == cardId && x.Pin.Equals(cardPin));

            return cardFound != null;
        }
    }
}