using System;
using System.Linq;
using System.Text;
using CsVendingMachine.Services;
using CsVendingMachine.Services.Implementation;
using CsVendingMachine.Types;
using Ninject;

namespace CsVendingMachine
{
    /// <summary>
    /// This is for testing purposes
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        { 
            var dataService = DependencyResolver.Kernel.Get<ITestDataRepositoryService>();
            dataService.CreateTestData();

            var stockCountService = new ProductStockCountService(dataService);
            var cardPinService = new CardPinService(dataService);
            var accountBalanceService = new AccountBalanceService(dataService);
            var productPriceService = new ProductPriceService(dataService);

            var productId = dataService.GetProducts().FirstOrDefault()?.Id;

            var vendService = new VendService(stockCountService, cardPinService, accountBalanceService, productPriceService);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Welcome to Vending Machine: Type Commands below example (card1 pin 1111)");

            Console.WriteLine(stringBuilder.ToString());

            while (true)
            {
                var line = Console.ReadLine();

                if (string.IsNullOrEmpty(line)) return;

                var output = line.Split(null);
                var cardType = output[0];
                var pin = output[2];

                Guid? cardId = null;

                if (cardType.Equals("card1"))
                {
                    cardId = dataService.GetCards().FirstOrDefault(x => x.Name.Equals("Card1"))?.Id;
                }
                else if (cardType.Equals("card2"))
                {
                    cardId = dataService.GetCards().FirstOrDefault(x => x.Name.Equals("Card2"))?.Id;
                }

                var result = vendService.ProcessVend(cardId.GetValueOrDefault(), pin, productId.GetValueOrDefault());

                Console.WriteLine(result.IsSuccess ? "Purchase successfull." : result.ErrorDescription);
            }
        }
    }
}
