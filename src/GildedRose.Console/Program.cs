namespace GildedRose.Console
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    

    using GildedRose.Logic;
    using GildedRose.Model;

    using Microsoft.Practices.Unity;

    public class Program
    {
        /// <summary>
        /// The IoC container/
        /// </summary>
        protected static IUnityContainer UnityContainer = new UnityContainer();

        public IList<StockItem> StockItems;

        // Goblin appeasment.
        public IList<Item> Items => this.StockItems.Select(si => si as Item).ToList();
        
        static void Main(string[] args)
        {
            // register dependencies
            UnityRegistration.RegisterDependencies(UnityContainer);

            System.Console.WriteLine("OMGHAI!");

            var app = new Program();
            
            app.LoadStock();
            
            foreach (var item in app.Items)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("|{0}|{1}|{2}|", item.Name, item.SellIn, item.Quality));
            }

            app.UpdateQuality();

            foreach (var item in app.Items)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("|{0}|{1}|{2}|", item.Name, item.SellIn, item.Quality));
            }

            app.SaveStock();

            System.Console.ReadKey();
        }
        
        /// <summary>
        /// Updates the quality and remaining shelf life of the stock.
        /// </summary>
        /// <param name="items">The current stock.</param>
        public void UpdateQuality()
        {
            IStockAgeingProcess stockAgeingProcess = UnityContainer.Resolve<IStockAgeingProcess>();

            stockAgeingProcess.RunStockAgeing(this.StockItems);
        }

        /// <summary>
        /// Loads the stock from storage.
        /// </summary>
        /// <returns>A Task.</returns>
        public void LoadStock()
        {
            IStockRepository stockRepository = UnityContainer.Resolve<IStockRepository>();

            // For proper implemenation create async context and await this.
            this.StockItems = stockRepository.LoadStockListAsync().Result;
        }

        public void SaveStock()
        {
            IStockRepository stockRepository = UnityContainer.Resolve<IStockRepository>();

            // For proper implemenation create async context and await this.
            stockRepository.SaveStockListAsync(this.StockItems).Wait(); 
        }

    }
}
