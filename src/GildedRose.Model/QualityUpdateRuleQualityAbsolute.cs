// <copyright file="QualityUpdateRuleQualityAbsolute.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>

namespace GildedRose.Model
{
    using System;

    /// <summary>
    /// Defines the quality update rule that sets the quality to the absolute value specified in the rule.
    /// </summary>
    /// <seealso cref="GildedRose.Model.QualityUpdateRule" />
    [Serializable]
    public class QualityUpdateRuleQualityAbsolute : QualityUpdateRule
    {
        /// <summary>
        /// Gets or sets the quality value that is applied when this rule is invoked.
        /// </summary>
        /// <value>
        /// The quality value.
        /// </value>
        public int QualityValue { get; set; }
    }
}
