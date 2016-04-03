// <copyright file="ProductCatalogueItem.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the product catalogue item class.
    /// </summary>
    /// <seealso cref="GildedRose.Model.Item" />
    [Serializable]
    public class ProductCatalogueItem : Item
    {
        /// <summary>
        /// Gets the quality adjustment rules for this item.
        /// </summary>
        /// <value>
        /// The quality adjustment rules.
        /// </value>
        public IList<QualityUpdateRule> QualityAdjustmentRules { get; } = new List<QualityUpdateRule>();
    }
}