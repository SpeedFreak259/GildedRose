// <copyright file="IStockAgeingProcess.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using System.Collections.Generic;

    using GildedRose.Model;

    /// <summary>
    /// Defines the public interface for the stock ageing process.
    /// </summary>
    public interface IStockAgeingProcess
    {
        /// <summary>
        /// Runs the stock ageing against the supplied stock list.
        /// </summary>
        /// <param name="stockItems">The stock items.</param>
        void RunStockAgeing(IEnumerable<StockItem> stockItems);
    }
}
