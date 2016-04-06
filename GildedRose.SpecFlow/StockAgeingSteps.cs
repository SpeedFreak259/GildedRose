using System;
using TechTalk.SpecFlow;

namespace GildedRose.SpecFlow
{
    using System;
    using System.Collections.Generic;

    using GildedRose.Logic;
    using GildedRose.Model;

    using Xunit;

    [Binding]
    public class StockAgeingSteps
    {
        private List<StockItem> storeStock = new List<StockItem>();
        private StockItem stockItem = new StockItem();
        private DateTime currentStoreDate;
        
        [Given(@"an item added to stock with starting quality of (.*) and shelf life of (.*) days")]
        public void GivenAnItemAddedToStockWithStartingQualityOfAndShelfLifeOfDays(int p0, int p1)
        {
            stockItem.Quality = p0;
            stockItem.SellIn = p1;
            stockItem.AddedToStockUtc = new DateTime(2016, 1, 1);
            storeStock.Add(stockItem);
        }
        
        [Given(@"the item degrades at (.*) quality point per day")]
        public void GivenTheItemDegradesAtQualityPointPerDay(int p0)
        {
            var degradingQuality = new QualityUpdateRuleQualityDelta();
            degradingQuality.QualityAdjustment = -p0;
            stockItem.QualityAdjustmentRules.Add(degradingQuality);
        }

        [Given(@"the item improves at (.*) quality point per day")]
        public void GivenTheItemImprovesAtQualityPointPerDay(int p0)
        {
            var improvingQuality = new QualityUpdateRuleQualityDelta();
            improvingQuality.QualityAdjustment = p0;
            stockItem.QualityAdjustmentRules.Add(improvingQuality);
        }

        [Given(@"a legendary item")]
        public void GivenALegendaryItem()
        {
            this.stockItem.SellIn = 0;
            this.stockItem.Quality = 80;
            this.stockItem.MaxQuality = 80;
            this.stockItem.SellInRule.DailyAdjustment = 0;
            this.stockItem.QualityAdjustmentRules.Clear();
        }
        
        [Given(@"when the remaining shelf life is between (.*) and (.*) days the quality degrades at (.*) points per day")]
        public void GivenWhenTheRemainingShelfLifeIsBetweenAndDaysTheQualityDegradesAtPointsPerDay(int p0, int p1, int p2)
        {
            var degradingQuality = new QualityUpdateRuleQualityDelta();
            degradingQuality.ActiveFromSellIn = p0;
            degradingQuality.ActiveUntilSellIn = p1;
            degradingQuality.QualityAdjustment = -p2;
            stockItem.QualityAdjustmentRules.Add(degradingQuality);
        }

        [Given(@"when the remaining shelf life is less than (.*) days then quality degrades at (.*) points per day")]
        public void GivenWhenTheRemainingShelfLifeIsLessThatDaysThenQualityDegradesAtPointsPerDay(int p0, int p1)
        {
            var degradingQuality = new QualityUpdateRuleQualityDelta();
            degradingQuality.ActiveFromSellIn = p0 - 1;
            degradingQuality.ActiveUntilSellIn = int.MinValue;
            degradingQuality.QualityAdjustment = -p1;
            stockItem.QualityAdjustmentRules.Add(degradingQuality);
        }

        [Given(@"when the remaining shelf life is greater than (.*) days the quality improves at (.*) points per day")]
        public void GivenWhenTheRemainingShelfLifeIsGreaterThanDaysTheQualityImprovesAtPointsPerDay(int p0, int p1)
        {
            var improvingQuality = new QualityUpdateRuleQualityDelta
                                    {
                                        ActiveUntilSellIn = p0 + 1,
                                        QualityAdjustment = p1
                                    };
            stockItem.QualityAdjustmentRules.Add(improvingQuality);

        }

        [Given(@"when the remaining shelf life is between (.*) and (.*) days the quality improves at (.*) points per day")]
        public void GivenWhenTheRemainingShelfLifeIsBetweenAndDaysTheQualityImprovesAtPointsPerDay(int p0, int p1, int p2)
        {
            var improvingQuality = new QualityUpdateRuleQualityDelta
            {
                ActiveFromSellIn = p0,
                ActiveUntilSellIn = p1,
                QualityAdjustment = p2
            };
            stockItem.QualityAdjustmentRules.Add(improvingQuality);
        }

        [Given(@"when the remaining shelf life is (.*) the quality becomes (.*)")]
        public void GivenWhenTheRemainingShelfLifeIsTheQualityBecomes(int p0, int p1)
        {
            var absoluteQualityRule = new QualityUpdateRuleQualityAbsolute
            {
                ActiveFromSellIn = p0,
                ActiveUntilSellIn = p0,
                QualityValue = p1
            };
            stockItem.QualityAdjustmentRules.Add(absoluteQualityRule);
        }


        [When(@"the item has been in stock for (.*) days")]
        public void WhenTheItemHasBeenInStockForDay(int p0)
        {
            this.currentStoreDate = stockItem.AddedToStockUtc.AddDays(p0);
            var stockAgeingProcess = new StockAgeingProcess(() => this.currentStoreDate, new QualityRuleProcessorFactory());

            stockAgeingProcess.RunStockAgeing(this.storeStock);    
        }

        [Then(@"the quality should be equal to (.*)")]
        public void ThenTheQualityShouldBeReducedTo(int p0)
        {
            Assert.Equal(p0, stockItem.Quality);
        }
        
        [Then(@"the remaining shelf life should be (.*)")]
        public void ThenTheRemainingShelfLifeShouldBe(int p0)
        {
            Assert.Equal(p0, stockItem.SellIn);
        }
    }
}
