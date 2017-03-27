using CsVendingMachine.Services;
using CsVendingMachine.Services.Implementation;
using Ninject.Modules;

namespace CsVendingMachine
{
    public class ModuleKernel : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountBalanceService>().To<AccountBalanceService>();
            Bind<ICardPinService>().To<CardPinService>();
            Bind<IProductStockCountService>().To<ProductStockCountService>();
            Bind<IProductPriceService>().To<ProductPriceService>();
            Bind<IVendService>().To<VendService>();
            Bind<ITestDataRepositoryService>().To<TestDataRepositoryService>();
        }
    }
}