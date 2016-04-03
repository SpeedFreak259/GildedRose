// <copyright file="QualityRuleProcessorAbsoluteTests.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Tests
{
    using System;

    using GildedRose.Logic;
    using GildedRose.Model;

    using Xunit;

    /// <summary>
    /// Defines the tests for the <see cref="QualityRuleProcessorAbsolute"/>.
    /// </summary>
    public class QualityRuleProcessorAbsoluteTests
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
        private static QualityUpdateRuleQualityAbsolute BlankRule => new QualityUpdateRuleQualityAbsolute();

        /// <summary>
        /// Gets a blank delta rule.
        /// </summary>
        /// <value>
        /// The delta rule.
        /// </value>
        private static QualityUpdateRuleQualityDelta DeltaRule => new QualityUpdateRuleQualityDelta();

        /// <summary>
        /// Asserts that the process rule is Guarded.
        /// </summary>
        [Fact]
        public void GivenNullRule_WhenProcessRule_ThenThrowsArgumentException()
        {
            // Arrange
            var ruleProcessor = new QualityRuleProcessorAbsolute();

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
            var ruleProcessor = new QualityRuleProcessorAbsolute();

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
            var ruleProcessore = new QualityRuleProcessorAbsolute();

            // Act, Assert
            Assert.Throws<ArgumentException>(() => ruleProcessore.ProcessRule(DeltaRule, BlankStockItem));
        }

        /// <summary>
        /// Asserts the when the rule is out of range the quality is not applied to the stock item.
        /// </summary>
        [Theory]
        [InlineData(20)]
        [InlineData(11)]
        [InlineData(6)]
        public void GivenAbsoluteRuleActiveBetweenDays10And7_WhenSellInIsOutsideRand_ThenQualityIsNotUpdated(int sellIn)
        {
            // Arrange
            var ruleProcessor = new QualityRuleProcessorAbsolute();
            var rule = new QualityUpdateRuleQualityAbsolute { ActiveFromSellIn = 10, ActiveUntilSellIn = 7, QualityValue = 100 };
            var stockItem = new StockItem { SellIn = sellIn, Quality = 0 };

            // Act
            ruleProcessor.ProcessRule(rule, stockItem);

            // Assert
            Assert.Equal(0, stockItem.Quality);
        }

        /// <summary>
        /// Asserts that rule is applied when the sell in is within the applicable range of the rule.
        /// </summary>
        /// <param name="sellIn">The sell in days.</param>
        [Theory]
        [InlineData(10)]
        [InlineData(9)]
        [InlineData(8)]
        [InlineData(7)]
        public void GivenAbsoluteRuleActiveBetweenDays10And7_WhenSellInIsBetween10And7_ThenQualityIsUpdated(int sellIn)
        {
            // Arrange
            var ruleProcessor = new QualityRuleProcessorAbsolute();
            var rule = new QualityUpdateRuleQualityAbsolute { ActiveFromSellIn = 10, ActiveUntilSellIn = 7, QualityValue = 100 };
            var stockItem = new StockItem { SellIn = sellIn, Quality = 0 };

            // Act
            ruleProcessor.ProcessRule(rule, stockItem);

            // Assert
            Assert.Equal(100, stockItem.Quality);
        }
    }
}
