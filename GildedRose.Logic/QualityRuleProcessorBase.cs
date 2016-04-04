// <copyright file="QualityRuleProcessorBase.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using System;

    using GildedRose.Model;
    using Microsoft.Practices.Unity.Utility;
    using Properties;

    /// <summary>
    /// Defined the base implementation of the quality rule processor.
    /// </summary>
    /// <typeparam name="TQualityRule">The type of the quality rule.</typeparam>
    public abstract class QualityRuleProcessorBase<TQualityRule>
        where TQualityRule : QualityUpdateRule
    {
        /// <summary>
        /// Processes the quality rule. Guards the types and validates the rule is within date range.
        /// </summary>
        /// <param name="rule">The quality update rule.</param>
        /// <param name="stockItem">The stock item.</param>
        /// <exception cref="System.ArgumentException">.</exception>
        public virtual void ProcessRule(QualityUpdateRule rule, StockItem stockItem)
        {
            Guard.ArgumentNotNull(rule, nameof(rule));
            Guard.ArgumentNotNull(stockItem, nameof(stockItem));

            TQualityRule ruleAbsolute = rule as TQualityRule;

            if (ruleAbsolute == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.QualityRuleCastError, rule.GetType().Name, typeof(QualityUpdateRuleQualityAbsolute).Name));
            }

            // Apply the rule if the SellIn value is in the applicable range.
            if ((stockItem.SellIn <= rule.ActiveFromSellIn)
               && (stockItem.SellIn >= rule.ActiveUntilSellIn))
            {
                this.ApplyRule(ruleAbsolute, stockItem);
            }
        }

        /// <summary>
        /// Apply the rule to the stock item.
        /// </summary>
        /// <param name="rule">The rule cast to the expected type.</param>
        /// <param name="stockItem">The stock item.</param>
        protected abstract void ApplyRule(TQualityRule rule, StockItem stockItem);
    }
}
