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
        protected override void ApplyRule()
        {
            this.StockItem.Quality = this.QualityRule.QualityValue;
        }
    }
}