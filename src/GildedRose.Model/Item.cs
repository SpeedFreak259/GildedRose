// <copyright file="Item.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
using System.Collections.Generic;

namespace GildedRose.Model
{
    /// <summary>
    /// Defines the Item entity.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of days the item must be sold in.
        /// </summary>
        /// <value>The sell in value.</value>
        public int SellIn { get; set; }

        /// <summary>
        /// Gets or sets the quality value.
        /// </summary>
        /// <value>The quality score for the item.
        /// </value>
        public int Quality { get; set; }
    }
}
