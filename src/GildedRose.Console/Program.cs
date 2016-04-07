// <copyright file="Program.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Console
{
    using System;    
    using System.Collections.Generic;
    using System.Linq;

    using GildedRose.Logic;
    using GildedRose.Model;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Defines the Gilded Rose hosting process and entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The IoC container/
        /// </summary>
        private static IUnityContainer unityContainer = new UnityContainer();

        /// <summary>
        /// The items currently in stock.
        /// </summary>
        private IList<StockItem> stockItems;

        /// <summary>
        /// Gets the stock items as there base type <see cref="Item"/>
        /// <para>
        /// Goblin appeasment - requirement to keep this type. Simply cast our stock list back to the base Item type.
        /// </para>
        /// </summary>
        public IList<Item> Items => this.stockItems.Select(si => si as Item).ToList();

        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            // register dependencies
            UnityRegistration.RegisterDependencies(unityContainer);

            System.Console.WriteLine("OMGHAI!");

            var app = new Program();

            app.LoadStock();

            // Write the stock list and quality to debug output.
            app.DumpStockToDebug();

            // run the daily stock quality adjustments.
            app.UpdateQuality();

            // Write the stock list and quality to debug output after quality adjustment.
            app.DumpStockToDebug();

            // update the stock datebase.
            app.SaveStock();

            System.Console.ReadKey();
        }

        /// <summary>
        /// Updates the quality and remaining shelf life of the stock.
        /// </summary>
        public void UpdateQuality()
        {
            IStockAgeingProcess stockAgeingProcess = unityContainer.Resolve<IStockAgeingProcess>();

            stockAgeingProcess.RunStockAgeing(this.stockItems);
        }

        /// <summary>
        /// Loads the stock from storage.
        /// </summary>
        public void LoadStock()
        {
            IStockRepository stockRepository = unityContainer.Resolve<IStockRepository>();

            // For proper implemenation create async context and await this.
            this.stockItems = stockRepository.LoadStockListAsync().Result;
        }

        /// <summary>
        /// Saves the updated stock to storage.
        /// </summary>
        public void SaveStock()
        {
            IStockRepository stockRepository = unityContainer.Resolve<IStockRepository>();

            // For proper implemenation create async context and await this.
            stockRepository.SaveStockListAsync(this.stockItems).Wait();
        }

        /// <summary>
        /// Dumps the stock to debug output.
        /// </summary>
        private void DumpStockToDebug()
        {
            // Output the state of the stock before update.
            foreach (var item in this.stockItems)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("|{0}|{1}|{2}|", item.Name, item.SellIn, item.Quality));
            }
        }
    }
}
