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
    public abstract class QualityRuleProcessorBase<TQualityRule> : IQualityRuleProcessor
        where TQualityRule : QualityUpdateRule
    {
        /// <summary>
        /// Gets the quality rule.
        /// </summary>
        /// <value>
        /// The quality rule cast to the specific implementation.
        /// </value>
        protected TQualityRule QualityRule { get; private set; }

        /// <summary>
        /// Gets the stock item.
        /// </summary>
        protected StockItem StockItem { get; private set; }

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

            this.StockItem = stockItem;
            this.QualityRule = rule as TQualityRule;

            if (this.QualityRule == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.QualityRuleCastError, rule.GetType().Name, typeof(QualityUpdateRuleQualityAbsolute).Name));
            }

            // Apply the rule if the SellIn value is in the applicable range.
            if ((stockItem.SellIn <= rule.ActiveFromSellIn)
               && (stockItem.SellIn >= rule.ActiveUntilSellIn))
            {
                this.ApplyRule();
            }
        }

        /// <summary>
        /// Apply the rule to the stock item.
        /// </summary>
        protected abstract void ApplyRule();
    }
}
