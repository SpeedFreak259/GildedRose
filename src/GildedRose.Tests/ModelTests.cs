namespace GildedRose.Tests
{
    using GildedRose.Model;

    using Xunit;

    /// <summary>
    /// Tests model objects.
    /// </summary>
    public class ModelTests
    {
        [Fact]
        /// <summary>
        /// Asserts that default constructor for ProductCatalogueItem defaults to daily sellin adjustment of 1.
        /// </summary>
        public void GivenCatalogueItem_WhenDefaultConstructor_ThenDailySellInIsOne()
        {
            // Arrange
            var item = new ProductCatalogueItem();

            // Assert
            Assert.NotNull(item.SellInRule);
            Assert.Equal(1, item.SellInRule.DailyAdjustment);
        }

        [Fact]
        /// <summary>
        /// Asserts that default constructor initialises the QualityAdjustmentRules list.
        /// </summary>
        public void GivenCatalogueItem_WhenDefaultConstructor_ThenRuleListInitliasedEmpty()
        {
            // Arrange
            var item = new ProductCatalogueItem();

            // Assert
            Assert.NotNull(item.QualityAdjustmentRules);
        }
    }
}