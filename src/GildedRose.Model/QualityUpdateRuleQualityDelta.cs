// <copyright file="QualityUpdateRuleQualityDelta.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>

namespace GildedRose.Model
{
    using System;

    /// <summary>
    /// Defines the quality update rule that adjusts the quality by the specified amount.
    /// </summary>
    /// <seealso cref="GildedRose.Model.QualityUpdateRule" />
    [Serializable]
    public class QualityUpdateRuleQualityDelta : QualityUpdateRule
    {
        /// <summary>
        /// Gets or sets the number of points to adjust the quality by.
        /// </summary>
        /// <value>
        /// The quality adjustment value.
        /// </value>
        public int QualityAdjustment { get; set; }
    }
}
