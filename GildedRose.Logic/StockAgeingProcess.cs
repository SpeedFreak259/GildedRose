// <copyright file="StockAgeingProcess.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using System;
    using System.Collections.Generic;

    using GildedRose.Model;

    using Microsoft.Practices.Unity.Utility;

    /// <summary>
    /// Defines the stock ageing process that applies the ageing to the stock items.
    /// </summary>
    public class StockAgeingProcess
    {
        /// <summary>
        /// Injectable clock instance.
        /// </summary>
        private readonly Func<DateTime> clock;

        /// <summary>
        /// The rule processor factory.
        /// </summary>
        private readonly QualityRuleProcessorFactory processorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockAgeingProcess"/> class.
        /// </summary>
        /// <param name="clock">The clock function.</param>
        /// <param name="ruleProcessorFactory">Instance of the rule processor factory.</param>
        public StockAgeingProcess(Func<DateTime> clock, QualityRuleProcessorFactory ruleProcessorFactory)
        {
            this.clock = clock;
            this.processorFactory = ruleProcessorFactory;
        }

        /// <summary>
        /// Runs the stock ageing against the supplied stock list.
        /// </summary>
        /// <param name="stockItems">The stock items.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Parameter is guarded.")]
        public void RunStockAgeing(IEnumerable<StockItem> stockItems)
        {
            Guard.ArgumentNotNull(stockItems, nameof(stockItems));

            foreach (StockItem item in stockItems)
            {
                this.AgeItem(item);
            }
        }

        /// <summary>
        /// Ages an individual stock item.
        /// </summary>
        /// <param name="item">The item.</param>
        private void AgeItem(StockItem item)
        {
            // find when the item was last updated.
            var lastUpdate = DateTimeHelper.MostRecent(item.AddedToStockUtc, item.QualityRecalculatedUtc);

            // bring the stock item up-to-date
            while (lastUpdate.Date < this.clock().Date)
            {
                // Update the sell in value according to the rule.
                item.SellIn -= item.SellInRule.DailyAdjustment;

                // iterate over the quality adjustment rules and apply them to the item.
                foreach (var rule in item.QualityAdjustmentRules)
                {
                    var processor = this.processorFactory.GetProcessorForRule(rule);
                    processor.ProcessRule(rule, item);
                }

                if (item.Quality > item.MaxQuality)
                {
                    item.Quality = item.MaxQuality;
                }

                // record when the quality calculation was correct.
                lastUpdate = lastUpdate.AddDays(1);
            }

            item.QualityRecalculatedUtc = lastUpdate;
        }
    }
}
