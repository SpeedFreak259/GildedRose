﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.0.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace GildedRose.SpecFlow
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class StockAgeingFeature : Xunit.IClassFixture<StockAgeingFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "StockAgeing.feature"
#line hidden
        
        public StockAgeingFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "StockAgeing", "\tIn order to manage the store stock, \r\n\tAs a store keeper\r\n\tI want the quality of" +
                    " the stock items to adjust daily as the product ages.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void SetFixture(StockAgeingFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "StockAgeing")]
        [Xunit.TraitAttribute("Description", "Most products degrade with age. After the sell by date is reached the product deg" +
            "rades twice as quickly. The minimum quality is zero.")]
        [Xunit.TraitAttribute("Category", "degradingProducts")]
        [Xunit.InlineDataAttribute("1", "9", "6", new string[0])]
        [Xunit.InlineDataAttribute("7", "3", "0", new string[0])]
        [Xunit.InlineDataAttribute("8", "1", "-1", new string[0])]
        [Xunit.InlineDataAttribute("9", "0", "-2", new string[0])]
        [Xunit.InlineDataAttribute("10", "0", "-3", new string[0])]
        public virtual void MostProductsDegradeWithAge_AfterTheSellByDateIsReachedTheProductDegradesTwiceAsQuickly_TheMinimumQualityIsZero_(string stockdays, string quality, string shelflife, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "degradingProducts"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Most products degrade with age. After the sell by date is reached the product deg" +
                    "rades twice as quickly. The minimum quality is zero.", @__tags);
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("an item added to stock with starting quality of 10 and shelf life of 7 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.And("when the remaining shelf life is between 7 and 0 days the quality degrades at 1 p" +
                    "oints per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("when the remaining shelf life is less than 0 days then quality degrades at 2 poin" +
                    "ts per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.When(string.Format("the item has been in stock for {0} days", stockdays), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
 testRunner.Then(string.Format("the quality should be equal to {0}", quality), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 14
 testRunner.And(string.Format("the remaining shelf life should be {0}", shelflife), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "StockAgeing")]
        [Xunit.TraitAttribute("Description", "Conjured products degrade quickly at 2 points per day.")]
        [Xunit.InlineDataAttribute("0", "20", "7", new string[0])]
        [Xunit.InlineDataAttribute("1", "18", "6", new string[0])]
        [Xunit.InlineDataAttribute("7", "6", "0", new string[0])]
        [Xunit.InlineDataAttribute("8", "2", "-1", new string[0])]
        [Xunit.InlineDataAttribute("9", "0", "-2", new string[0])]
        [Xunit.InlineDataAttribute("10", "0", "-3", new string[0])]
        public virtual void ConjuredProductsDegradeQuicklyAt2PointsPerDay_(string stockdays, string quality, string shelflife, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Conjured products degrade quickly at 2 points per day.", exampleTags);
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("an item added to stock with starting quality of 20 and shelf life of 7 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
 testRunner.And("when the remaining shelf life is between 20 and 0 days the quality degrades at 2 " +
                    "points per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.And("when the remaining shelf life is less than 0 days then quality degrades at 4 poin" +
                    "ts per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.When(string.Format("the item has been in stock for {0} days", stockdays), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
 testRunner.Then(string.Format("the quality should be equal to {0}", quality), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 30
 testRunner.And(string.Format("the remaining shelf life should be {0}", shelflife), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "StockAgeing")]
        [Xunit.TraitAttribute("Description", "Certain products improve with age, brie for example")]
        [Xunit.TraitAttribute("Category", "improvingProducts")]
        [Xunit.InlineDataAttribute("1", "2", "6", new string[0])]
        [Xunit.InlineDataAttribute("7", "8", "0", new string[0])]
        [Xunit.InlineDataAttribute("8", "9", "-1", new string[0])]
        public virtual void CertainProductsImproveWithAgeBrieForExample(string stockdays, string quality, string remainShelfLife, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "improvingProducts"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Certain products improve with age, brie for example", @__tags);
#line 44
this.ScenarioSetup(scenarioInfo);
#line 45
 testRunner.Given("an item added to stock with starting quality of 1 and shelf life of 7 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 46
 testRunner.And("the item improves at 1 quality point per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.When(string.Format("the item has been in stock for {0} days", stockdays), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 48
 testRunner.Then(string.Format("the quality should be equal to {0}", quality), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 49
 testRunner.And(string.Format("the remaining shelf life should be {0}", remainShelfLife), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "StockAgeing")]
        [Xunit.TraitAttribute("Description", "Add an improving item to stock check the quality reaches a maximum value")]
        public virtual void AddAnImprovingItemToStockCheckTheQualityReachesAMaximumValue()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add an improving item to stock check the quality reaches a maximum value", ((string[])(null)));
#line 58
this.ScenarioSetup(scenarioInfo);
#line 59
 testRunner.Given("an item added to stock with starting quality of 1 and shelf life of 7 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 60
 testRunner.And("the item improves at 1 quality point per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.When("the item has been in stock for 100 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 62
 testRunner.Then("the quality should be equal to 50", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 63
 testRunner.And("the remaining shelf life should be -93", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "StockAgeing")]
        [Xunit.TraitAttribute("Description", "A concert ticket improves in quality more quickly as the concert approaches and d" +
            "rops to zero after the concert")]
        [Xunit.TraitAttribute("Category", "concertTickets")]
        [Xunit.InlineDataAttribute("1", "2", "19", new string[0])]
        [Xunit.InlineDataAttribute("9", "10", "11", new string[0])]
        [Xunit.InlineDataAttribute("15", "23", "5", new string[0])]
        [Xunit.InlineDataAttribute("20", "38", "0", new string[0])]
        [Xunit.InlineDataAttribute("21", "0", "-1", new string[0])]
        public virtual void AConcertTicketImprovesInQualityMoreQuicklyAsTheConcertApproachesAndDropsToZeroAfterTheConcert(string stockdays, string quality, string daysToConcert, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "concertTickets"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A concert ticket improves in quality more quickly as the concert approaches and d" +
                    "rops to zero after the concert", @__tags);
#line 67
this.ScenarioSetup(scenarioInfo);
#line 68
 testRunner.Given("an item added to stock with starting quality of 1 and shelf life of 20 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 69
 testRunner.And("when the remaining shelf life is greater than 10 days the quality improves at 1 p" +
                    "oints per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.And("when the remaining shelf life is between 10 and 6 days the quality improves at 2 " +
                    "points per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And("when the remaining shelf life is between 5 and 0 days the quality improves at 3 p" +
                    "oints per day", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("when the remaining shelf life is -1 the quality becomes 0", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.When(string.Format("the item has been in stock for {0} days", stockdays), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 74
 testRunner.Then(string.Format("the quality should be equal to {0}", quality), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 75
 testRunner.And(string.Format("the remaining shelf life should be {0}", daysToConcert), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "StockAgeing")]
        [Xunit.TraitAttribute("Description", "One product is legendary, its quality remains constant and the remaining shelf li" +
            "fe does not adjust regardless of time on shelf")]
        [Xunit.TraitAttribute("Category", "legendaryProducts")]
        [Xunit.InlineDataAttribute("1", "80", "0", new string[0])]
        [Xunit.InlineDataAttribute("2", "80", "0", new string[0])]
        [Xunit.InlineDataAttribute("10", "80", "0", new string[0])]
        [Xunit.InlineDataAttribute("365", "80", "0", new string[0])]
        [Xunit.InlineDataAttribute("3650", "80", "0", new string[0])]
        public virtual void OneProductIsLegendaryItsQualityRemainsConstantAndTheRemainingShelfLifeDoesNotAdjustRegardlessOfTimeOnShelf(string stockdays, string quality, string shelflife, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "legendaryProducts"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("One product is legendary, its quality remains constant and the remaining shelf li" +
                    "fe does not adjust regardless of time on shelf", @__tags);
#line 87
this.ScenarioSetup(scenarioInfo);
#line 88
 testRunner.Given("a legendary item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 89
 testRunner.When(string.Format("the item has been in stock for {0} days", stockdays), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 90
 testRunner.Then(string.Format("the quality should be equal to {0}", quality), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 91
 testRunner.And(string.Format("the remaining shelf life should be {0}", shelflife), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                StockAgeingFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                StockAgeingFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion