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
            stockItem.AddedToStockUtc = DateTime.UtcNow;
            storeStock.Add(stockItem);
        }
        
        [Given(@"the item degrades at (.*) quality point per day")]
        public void GivenTheItemDegradesAtQualityPointPerDay(int p0)
        {
            var degradingQuality = new QualityUpdateRuleQualityDelta();
            degradingQuality.QualityAdjustment = -p0;
            stockItem.QualityAdjustmentRules.Add(degradingQuality);
        }
        
        [When(@"the item has been in stock for (.*) day")]
        public void WhenTheItemHasBeenInStockForDay(int p0)
        {
            this.currentStoreDate = stockItem.AddedToStockUtc.AddDays(p0);
            var stockAgeingProcess = new StockAgeingProcess(() => this.currentStoreDate, new QualityRuleProcessorFactory());

            stockAgeingProcess.RunStockAgeing(this.storeStock);
            
        }
        
        [Then(@"the quality should be reduced to (.*)")]
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
