// <copyright file="QualityRuleProcessorDelta.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using GildedRose.Model;

    /// <summary>
    /// Defines the process for QualityUpdateRuleQualityAbsolute rules.
    /// </summary>
    /// <seealso cref="GildedRose.Logic.QualityRuleProcessorBase{T}" />
    public class QualityRuleProcessorDelta : QualityRuleProcessorBase<QualityUpdateRuleQualityDelta>
    {
        /// <summary>
        /// Applies the delta rule by adjusting the stock item quality by the quality adjustment stored by the rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="stockItem">The stock item.</param>
        protected override void ApplyRule(QualityUpdateRuleQualityDelta rule, StockItem stockItem)
        {
            stockItem.Quality += rule.QualityAdjustment;
        }
    }
}
