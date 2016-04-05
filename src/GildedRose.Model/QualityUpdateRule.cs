// <copyright file="QualityUpdateRule.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>

namespace GildedRose.Model
{
    using System;

    /// <summary>
    /// Defines the base type for quality udpate rules.
    /// </summary>
    [Serializable]
    public abstract class QualityUpdateRule
    {
        /// <summary>
        /// Gets or sets the remaining SellIn days when this rule becomes active.
        /// </summary>
        /// <value>
        /// The number of days from SellIn when this rule becomes active.
        /// </value>
        public int ActiveFromSellIn { get; set; } = int.MaxValue;

        /// <summary>
        /// Gets or sets the remaining SellIn days when this rule becomes inactive
        /// </summary>
        /// <value>
        /// The number of days from SellIn when this rule becomes inactive.
        /// </value>
        public int ActiveUntilSellIn { get; set; } = int.MinValue;
    }
}
