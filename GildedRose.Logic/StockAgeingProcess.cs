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
        /// Initializes a new instance of the <see cref="StockAgeingProcess"/> class.
        /// </summary>
        /// <param name="clock">The clock function.</param>
        public StockAgeingProcess(Func<DateTime> clock)
        {
            this.clock = clock;
        }

        /// <summary>
        /// Runs the stock ageing against the supplied stock list.
        /// </summary>
        /// <param name="stockItems">The stock items.</param>
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
                    var processor = QualityRuleProcessorFactory.GetProcessorForRule(rule);
                    processor.ProcessRule(rule, item);
                }

                // record when the quality calculation was correct.
                lastUpdate = lastUpdate.AddDays(1);
            }

            item.QualityRecalculatedUtc = lastUpdate;
        }
    }
}
