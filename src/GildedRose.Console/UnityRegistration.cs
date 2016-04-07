// <copyright file="UnityRegistration.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Console
{
    using System;
    using System.Configuration;

    using GildedRose.Logic;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines the unit registration.
    /// </summary>
    public static class UnityRegistration
    {
        /// <summary>
        /// Registers the dependencies with unity.
        /// </summary>
        /// <param name="container">Instance of the unity container.</param>
        public static void RegisterDependencies(IUnityContainer container)
        {
            var stockfile = ConfigurationManager.AppSettings["stockfile"];

            // Use the json file stock repository and initliase with the stock file defined in app.config.
            container.RegisterType<IStockRepository, JsonFileStockRepository>(new InjectionConstructor(stockfile));
            container.RegisterType<QualityRuleProcessorFactory, QualityRuleProcessorFactory>();

            // Register the clock
            container.RegisterInstance<Func<DateTime>>(() => DateTime.UtcNow);
            container.RegisterType<IStockAgeingProcess, StockAgeingProcess>();
        }
    }
}
