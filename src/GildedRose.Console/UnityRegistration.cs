namespace GildedRose.Console
{
    using System;
    using System.Configuration;

    using GildedRose.Logic;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines the unit registration.
    /// </summary>
    static class UnityRegistration
    {
        /// <summary>
        /// Registers the dependencies with unity.
        /// </summary>
        /// <returns></returns>
        public static void RegisterDependencies(IUnityContainer container)
        {
            var stockfile = ConfigurationManager.AppSettings["stockfile"];

            container.RegisterType<IStockRepository, JsonFileStockRepository>(new InjectionConstructor(stockfile));
            container.RegisterType<QualityRuleProcessorFactory, QualityRuleProcessorFactory>();

            
            container.RegisterInstance<Func<DateTime>>(() => DateTime.UtcNow);
            container.RegisterType<IStockAgeingProcess, StockAgeingProcess>();
        }
    }
}
