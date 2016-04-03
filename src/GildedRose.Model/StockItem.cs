// <copyright file="StockItem.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Model
{
    using System;

    /// <summary>
    /// Defines the StockItem type that represents in instance of an item in stock.
    /// </summary>
    /// <seealso cref="GildedRose.Model.ProductCatalogueItem" />
    [Serializable]
    public class StockItem : ProductCatalogueItem
    {
        /// <summary>
        /// Gets or sets when the item was added to stock in Utc time.
        /// </summary>
        /// <value>
        /// The Utc <see cref="DateTime"/> when added to stock.
        /// </value>
        public DateTime AddedToStockUtc { get; set; }

        /// <summary>
        /// Gets or sets the date when the quality was last recalculated in Utc.
        /// </summary>
        /// <value>
        /// The quality recalculated date.
        /// </value>
        public DateTime QualityRecalculatedUtc { get; set; }
    }
}