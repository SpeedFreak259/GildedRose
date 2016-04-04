// <copyright file="QualityRuleProcessorAbsolute.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using GildedRose.Model;

    /// <summary>
    /// Defines the process for QualityUpdateRuleQualityAbsolute rules.
    /// </summary>
    /// <seealso cref="GildedRose.Logic.QualityRuleProcessorBase{T}" />
    public class QualityRuleProcessorAbsolute : QualityRuleProcessorBase<QualityUpdateRuleQualityAbsolute>
    {
        /// <summary>
        /// Applies the rule.
        /// </summary>
        /// <param name="rule">The rule definition.</param>
        /// <param name="stockItem">The stock item.</param>
        protected override void ApplyRule(QualityUpdateRuleQualityAbsolute rule, StockItem stockItem)
        {
            stockItem.Quality = rule.QualityValue;
        }
    }
}