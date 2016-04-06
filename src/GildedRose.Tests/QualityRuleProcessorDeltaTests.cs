namespace GildedRose.Tests
{
    using System;

    using GildedRose.Logic;
    using GildedRose.Model;

    using Xunit;

    /// <summary>
    /// Defines the tests for the <see cref="QualityRuleProcessorDelta"/> processor.
    /// </summary>
    public class QualityRuleProcessorDeltaTests
    {
        /// <summary>
        /// Gets the blank stock item.
        /// </summary>
        /// <value>
        /// The blank stock item.
        /// </value>
        private static StockItem BlankStockItem => new StockItem();

        /// <summary>
        /// Gets the blank rule.
        /// </summary>
        /// <value>
        /// The blank rule.
        /// </value>
        private static QualityUpdateRuleQualityDelta BlankRule => new QualityUpdateRuleQualityDelta();

        /// <summary>
        /// Gets a blank delta rule.
        /// </summary>
        /// <value>
        /// The delta rule.
        /// </value>
        private static QualityUpdateRuleQualityAbsolute AbsoluteRule => new QualityUpdateRuleQualityAbsolute();

        /// <summary>
        /// Asserts that the process rule is Guarded.
        /// </summary>
        [Fact]
        public void GivenNullRule_WhenProcessRule_ThenThrowsArgumentException()
        {
            // Arrange
            var ruleProcessor = new QualityRuleProcessorDelta();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => ruleProcessor.ProcessRule(null, BlankStockItem));
        }

        /// <summary>
        /// Asserts that the stock item is Guarded.
        /// </summary>
        [Fact]
        public void GivenNullStockItem_WhenProcessRule_ThenThrowsArgumentException()
        {
            // Arrange
            var ruleProcessor = new QualityRuleProcessorDelta();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => ruleProcessor.ProcessRule(BlankRule, null));
        }

        /// <summary>
        /// Asserts that the rule processor guards against receiving incorrect derived rule type.
        /// </summary>
        [Fact]
        public void GivenUnexpectedRuleType_WhenProcessRule_ThenThrowsArgumentException()
        {
            // Arrange
            var ruleProcessore = new QualityRuleProcessorDelta();

            // Act, Assert
            Assert.Throws<ArgumentException>(() => ruleProcessore.ProcessRule(AbsoluteRule, BlankStockItem));
        }

        /// <summary>
        /// Asserts the when the rule is out of range the quality is not applied to the stock item.
        /// </summary>
        /// <param name="sellIn">Simulation of the number of sell in days remaining for the stock item.</param>
        [Theory]
        [InlineData(20)]
        [InlineData(11)]
        [InlineData(6)]
        public void GivenAbsoluteRuleActiveBetweenDays10And7_WhenSellInIsOutsideRand_ThenQualityIsNotUpdated(int sellIn)
        {
            // Arrange
            var ruleProcessor = new QualityRuleProcessorDelta();
            var rule = new QualityUpdateRuleQualityDelta { ActiveFromSellIn = 10, ActiveUntilSellIn = 7, QualityAdjustment = -1 };
            var stockItem = new StockItem { SellIn = sellIn, Quality = 0 };

            // Act
            ruleProcessor.ProcessRule(rule, stockItem);

            // Assert
            Assert.Equal(0, stockItem.Quality);
        }

        /// <summary>
        /// Asserts that rule is applied when the sell in is within the applicable range of the rule.
        /// </summary>
        /// <param name="sellIn">Simulation of the number of sell in days remaining for the stock item.</param>
        [Theory]
        [InlineData(10)]
        [InlineData(9)]
        [InlineData(8)]
        [InlineData(7)]
        public void GivenAbsoluteRuleActiveBetweenDays10And7_WhenSellInIsBetween10And7_ThenQualityIsUpdated(int sellIn)
        {
            // Arrange
            var ruleProcessor = new QualityRuleProcessorDelta();
            var rule = new QualityUpdateRuleQualityDelta { ActiveFromSellIn = 10, ActiveUntilSellIn = 7, QualityAdjustment = -1 };
            var stockItem = new StockItem { SellIn = sellIn, Quality = 20 };

            // Act
            ruleProcessor.ProcessRule(rule, stockItem);

            // Assert
            Assert.Equal(19, stockItem.Quality);
        }
    }
}
