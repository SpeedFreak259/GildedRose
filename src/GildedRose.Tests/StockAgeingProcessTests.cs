namespace GildedRose.Tests
{
    using System;
    using System.Collections.Generic;

    using GildedRose.Logic;
    using GildedRose.Model;

    using Xunit;

    public class StockAgeingProcessTests
    {
        #region Theory Expectations

        /// <summary>
        /// Gets the bread ageing expectations.
        /// </summary>
        /// <value>
        /// XUnit thoery values to age the bread stock item over a period of time.
        /// </value>
        public static IEnumerable<object[]> BreadAgeingExpectation
        {
            get
            {
                return new[]
                {
                    new object[] { new DateTime(2016, 1, 1), 10, 7 },
                    new object[] { new DateTime(2016, 1, 2), 9, 6 },
                    new object[] { new DateTime(2016, 1, 3), 8, 5 },
                    new object[] { new DateTime(2016, 1, 4), 7, 4 },
                    new object[] { new DateTime(2016, 1, 5), 6, 3 },
                    new object[] { new DateTime(2016, 1, 6), 5, 2 },
                    new object[] { new DateTime(2016, 1, 7), 4, 1 },
                    new object[] { new DateTime(2016, 1, 8), 3, 0 },
                    new object[] { new DateTime(2016, 1, 9), 1, -1 },
                    new object[] { new DateTime(2016, 1, 10), -1, -2 }
                };
            }
        }

        #endregion

        /// <summary>
        /// Tests that stock ageing a standard item observes the ageing rules.
        /// </summary>
        /// <param name="ageingDate">The ageing date.</param>
        /// <param name="expectedQuality">The expected quality.</param>
        /// <param name="expectedSellIn">The expected sell in.</param>
        [Theory]
        [MemberData("BreadAgeingExpectation")]
        public void GivenAStandardProduct_WhenStockAged_ThenRulesApplied(DateTime ageingDate, int expectedQuality, int expectedSellIn)
        {
            var stockDate = new DateTime(2016, 1, 1);
            var ageDate = ageingDate;

            var ruleProcessorFactory = new QualityRuleProcessorFactory();
            var stock = this.GetBreadStockList(stockDate);

            var ageingProcess = new StockAgeingProcess(() => ageDate, ruleProcessorFactory);

            ageingProcess.RunStockAgeing(stock);

            Assert.Equal(expectedQuality, stock[0].Quality);
            Assert.Equal(expectedSellIn, stock[0].SellIn);
        }

        /// <summary>
        /// Gets a stock list containing a bread product.
        /// </summary>
        /// <param name="itemAddedToStockUtc">The item added to stock UTC.</param>
        /// <returns>A stock list containing a single bread item.</returns>
        private List<StockItem> GetBreadStockList(DateTime itemAddedToStockUtc)
        {
            var item = new StockItem
            {
                Name = "Bread",
                SellIn = 7,
                AddedToStockUtc = itemAddedToStockUtc,
                Quality = 10
            };

            item.QualityAdjustmentRules.Add(
                new QualityUpdateRuleQualityDelta
                {
                    ActiveUntilSellIn = 0,
                    QualityAdjustment = -1
                });

            item.QualityAdjustmentRules.Add(
                new QualityUpdateRuleQualityDelta
                {
                    ActiveFromSellIn = -1,
                    QualityAdjustment = -2
                });

            return new List<StockItem> { item };
        }
    }
}
