// <copyright file="UpdateQualityTests.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Tests
{
    using System.Collections.Generic;

    using GildedRose.Console;
    using GildedRose.Model;

    using Xunit;

    using static GildedRose.Console.Program;

    /// <summary>
    /// Defines the tests for the quality rules.
    /// </summary>
    public class UpdateQualityTests
    {
        /// <summary>
        /// The item name for an item that obeys the standard depreciating quality rule.
        /// </summary>
        private const string ItemNameStandardItem = "Elixir of the Mongoose";

        /// <summary>
        /// The item name for an item that improves with age.
        /// </summary>
        private const string ItemNameImprovesWithAge = "Aged Brie";

        /// <summary>
        /// The item name for an item with fixed quality.
        /// </summary>
        private const string ItemNameFixedQuality = "Sulfuras, Hand of Ragnaros";

        /// <summary>
        /// The item name for an item with accelerating improvement in quality.
        /// </summary>
        private const string ItemNameExpiringItem = "Backstage passes to a TAFKAL80ETC concert";

        /// <summary>
        /// The item name for an item with an accelerated rate of depreciating quality.
        /// </summary>
        private const string ItemNameAccerleratedDecline = "Conjured Mana Cake";

        /// <summary>
        /// The default sell in duration.
        /// </summary>
        private const int DefaultSellIn = 10;

        /// <summary>
        /// The default quality value.
        /// </summary>
        private const int DefaultQuality = 20;

        /// <summary>
        /// The maximum quality value.
        /// </summary>
        private const int MaxQuality = 50;

        /// <summary>
        /// The quality value for legendary products.
        /// </summary>
        private const int LegendaryQuality = 80;

        /// <summary>
        /// Asserts that UpdateQuality decrements quality by one for a standard product within sell by date.
        /// </summary>
        [Fact]
        public void GivenAStandardItemWithinSellBy_WhenUpdateQuality_QualityDecreasesByOne()
        {
            this.TestQualityAdjustment(ItemNameStandardItem, DefaultSellIn, DefaultQuality, DefaultQuality - 1);
        }

        /// <summary>
        /// Asserts that UpdateQuality does not decrement quality once it reaches zero.
        /// </summary>
        [Fact]
        public void GivenAStandardItemWithZeroQualityWithinSellBy_WhenUpdateQuality_QualityRemainsAtZero()
        {
            this.TestQualityAdjustment(ItemNameStandardItem, DefaultSellIn, 0, 0);
        }

        /// <summary>
        /// Assert that quality decrements twice as quickly once sell by reaches zero.
        /// </summary>
        [Fact]
        public void GivenAStandardItemWithZeroSellIn_WhenUpdateQuality_ThenQualityDecrementsByTwoPoints()
        {
            this.TestQualityAdjustment(ItemNameStandardItem, 0, DefaultQuality, DefaultQuality - 2);
        }

        /// <summary>
        /// Assert that sell in is allowed to go negative.
        /// </summary>
        [Fact]
        public void GivenAStandardItemWithZeroSellIn_WhenUpdateQuality_ThenSellInGoesNegative()
        {
            this.TestSellInAdjustment(ItemNameStandardItem, DefaultSellIn, DefaultSellIn - 1);
        }

        /// <summary>
        /// Asserts that products that improve with age increase in quality after calling UpdateQuality.
        /// </summary>
        [Fact]
        public void GivenImprovingItemWithPositiveSellIn_WhenUpdateQuality_ThenQualityIncreasesByOne()
        {
            this.TestQualityAdjustment(ItemNameImprovesWithAge, DefaultSellIn, 0, 1);
        }

        /// <summary>
        /// Asserts that products that improve with age do not increase in quality once maximum quality is reached.
        /// </summary>
        [Fact]
        public void GivenImprovingItemWithMaximumQuality_WhenUpdateQuality_ThenQualityRemainsAtMaximum()
        {
            this.TestQualityAdjustment(ItemNameImprovesWithAge, DefaultSellIn, MaxQuality, MaxQuality);
        }

        /// <summary>
        /// Asserts that improving product sell in decrements.
        /// </summary>
        [Fact]
        public void GivenImprovingItem_WhenUpdateQuality_ThenSellInIsDecremented()
        {
            this.TestSellInAdjustment(ItemNameImprovesWithAge, DefaultSellIn, DefaultSellIn - 1);
        }

        /// <summary>
        /// Assert that items that improve with age continue to improved when sell in reaches 0.
        /// </summary>
        [Fact]
        public void GivenImprovingItemWithZeroSellInRemaining_WhenUpdateQuality_ThenQualityContinuesToIncrease()
        {
            // See notes, quality increses by 2 points after SellIn reaches 0. Inconsistent with spec.
            this.TestQualityAdjustment(ItemNameImprovesWithAge, -1, 10, 12);
        }

        /// <summary>
        /// Asserts that items of fixed quality, with remaining SellIn do not have quality adjustments.
        /// </summary>
        [Fact]
        public void GivenFixedQualityItemWithRemainingSellIn_WhenUpdateQuality_ThenQualityRemainsAtInitialValue()
        {
            this.TestQualityAdjustment(ItemNameFixedQuality, DefaultSellIn, LegendaryQuality, LegendaryQuality);
        }

        /// <summary>
        /// Assert that a fixed quality item with zero remaining SellIn remains at initial quality.
        /// </summary>
        [Fact]
        public void GivenFixedQualityWithZeroRemainingSellIn_WhenUpdateQuality_ThenQualityRemainsAtInitialValue()
        {
            this.TestQualityAdjustment(ItemNameFixedQuality, 0, DefaultQuality, DefaultQuality);
        }

        /// <summary>
        /// Assert that sell in decrements for fixed quality item.
        /// </summary>
        [Fact]
        public void GivenFixedQualityItem_WhenUpdateQuality_ThenSellInIsNotDecremented()
        {
            this.TestSellInAdjustment(ItemNameFixedQuality, DefaultSellIn, DefaultSellIn);
        }

        /// <summary>
        /// Assert that expiring item with 20 days remaining gains one quality point each day.
        /// </summary>
        [Fact]
        public void GivenExpiringItemWith20Days_WhenUpdateQuality_QualityIncreasesByOnePoint()
        {
            this.TestQualityAdjustment(ItemNameExpiringItem, 20, 0, 1);
        }

        /// <summary>
        /// Assert that expiring item with 10 days remaining gains two quality points each day.
        /// </summary>
        [Fact]
        public void GivenExpiringItemWith10Days_WhenUpdateQuality_QualityIncreasesByTwoPoints()
        {
            this.TestQualityAdjustment(ItemNameExpiringItem, 10, 0, 2);
        }

        /// <summary>
        /// Assert that expiring item with 5 days remaining increases by three quality points each day.
        /// </summary>
        [Fact]
        public void GivenExpiringItemWith5Days_WhenUpdateQuality_QualityIncreasesByThreePoints()
        {
            this.TestQualityAdjustment(ItemNameExpiringItem, 5, 0, 3);
        }

        /// <summary>
        /// Assert that expiring item quality drops to zero when item expires.
        /// </summary>
        [Fact]
        public void GivenExpiringItemWith0Days_WhenUpdateQuality_QualityDropsToZero()
        {
            this.TestQualityAdjustment(ItemNameExpiringItem, 0, 10, 0);
        }

        /// <summary>
        /// Assert that expiring item with 20 days remaining at maximum quality does not exceed maximum quality.
        /// </summary>
        [Fact]
        public void GivenExpiringItemWith20DaysMaxQuality_WhenUpdateQuality_QualityDoesNotExceedMaximum()
        {
            this.TestQualityAdjustment(ItemNameExpiringItem, 20, MaxQuality, MaxQuality);
        }

        /// <summary>
        /// Assert that expiring item with 10 days remaining at maximum quality does not exceed maximum quality.
        /// </summary>
        [Fact]
        public void GivenExpiringItemWith10DaysMaxQuality_WhenUpdateQuality_QualityDoesNotExceedMaximum()
        {
            this.TestQualityAdjustment(ItemNameExpiringItem, 10, MaxQuality, MaxQuality);
        }

        /// <summary>
        /// Assert that expiring item with 5 days remaining at maximum quality does not exceed maximum quality.
        /// </summary>
        [Fact]
        public void GivenExpiringItemWith5DaysMaxQuality_WhenUpdateQuality_QualityDoesNotExceedMaximum()
        {
            this.TestQualityAdjustment(ItemNameExpiringItem, 5, MaxQuality, MaxQuality);
        }

        /// <summary>
        /// Implements the quality adjustment tests.
        /// </summary>
        /// <param name="itemName">Name of the item under test.</param>
        /// <param name="sellIn">The remaining SellIn days.</param>
        /// <param name="initialQuality">The initial quality score.</param>
        /// <param name="expectedQuality">The expected quality score after calling Update Quality.</param>
        private void TestQualityAdjustment(string itemName, int sellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            var items = this.GetSingleItemList(itemName, sellIn, initialQuality);

            // Act
            UpdateQuality(items);

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        /// <summary>
        /// Asserts the SellIn value is adjusted as expected.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="initialSellIn">The initial sell in value.</param>
        /// <param name="expectedSellIn">The expected sell in after UpdateQuality.</param>
        private void TestSellInAdjustment(string itemName, int initialSellIn, int expectedSellIn)
        {
            // Arrange
            var items = this.GetSingleItemList(itemName, initialSellIn);

            // Act
            UpdateQuality(items);

            // Assert
            Assert.Equal(expectedSellIn, items[0].SellIn);
        }

        /// <summary>
        /// Gets an item list containing a single item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="sellIn">The sell in value, defaults to 10.</param>
        /// <param name="quality">The quality value, defaults to 20.</param>
        /// <returns>
        /// An item list populated with single item.
        /// </returns>
        private List<Item> GetSingleItemList(string itemName, int sellIn = 10, int quality = 20)
        {
            var item = new Item
            {
                Name = itemName,
                SellIn = sellIn,
                Quality = quality
            };

            var items = new List<Item> { item };

            return items;
        }

        /// <summary>
        /// Gets the initial catalogue.
        /// </summary>
        /// <returns>The list of items in the initial product catalogue.</returns>
        private IList<Item> GetInitialCatalogue()
        {
            return new List<Item>
            {
                            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                            new Item
                                {
                                    Name = "Backstage passes to a TAFKAL80ETC concert",
                                    SellIn = 15,
                                    Quality = 20
                                },
                            new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };
        }
    }
}