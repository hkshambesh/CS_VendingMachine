using System;
using System.Reflection;
using Ninject;

namespace CsVendingMachine
{
    /// <summary>
    /// singleton class to resolve ninject modules 
    /// </summary>
    public static class DependencyResolver
    {
        private static readonly Lazy<IKernel> Instance = new Lazy<IKernel>(() =>
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            return kernel;
        });

        public static IKernel Kernel => Instance.Value;
    }
}