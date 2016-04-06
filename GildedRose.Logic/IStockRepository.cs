// <copyright file="IStockRepository.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>

namespace GildedRose.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GildedRose.Model;

    /// <summary>
    /// Defines the repository interface for the stock inventory.
    /// </summary>
    public interface IStockRepository
    {
        /// <summary>
        /// Loads the stock list.
        /// </summary>
        /// <returns>Returns a <see cref="IList{T}"/> of <see cref="StockItem"/>.</returns>
        Task<IList<StockItem>> LoadStockList();

        /// <summary>
        /// Saves the stock list.
        /// </summary>
        /// <param name="stockList">The stock items to persist.</param>
        /// <returns>A task of bool.</returns>
        Task<bool> SaveStockList(IEnumerable<StockItem> stockList);
    }
}
