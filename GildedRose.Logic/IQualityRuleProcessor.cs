// <copyright file="IQualityRuleProcessor.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using GildedRose.Model;

    /// <summary>
    /// Defines the rule processor interface.
    /// </summary>
    public interface IQualityRuleProcessor
    {
        /// <summary>
        /// Processes the rule on the stock item.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="stockItem">The stock item.</param>
        void ProcessRule(QualityUpdateRule rule, StockItem stockItem);
    }
}