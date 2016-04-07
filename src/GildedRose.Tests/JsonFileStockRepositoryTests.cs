namespace GildedRose.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;

    using GildedRose.Logic;
    using GildedRose.Model;

    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="JsonFileStockRepository"/> class.
    /// </summary>
    public class JsonFileStockRepositoryTests
    {
        /// <summary>
        /// The test stock file name
        /// </summary>
        private const string TestStockFileName = "stocktest.json";

        /// <summary>
        /// The reference stock file
        /// </summary>
        private const string ReferenceStockFile = "referenceStock.json";

        /// <summary>
        /// Gets the test stock date.
        /// </summary>
        /// <value>
        /// The test stock date.
        /// </value>
        private static DateTime TestStockDate => new DateTime(2016, 1, 1);

        /// <summary>
        /// Asserts that the sample stock list is persisted correctly to a file.
        /// </summary>
        [Fact]
        public void GivenSampleStockList_WhenSaveStockList_ThenRepositoryIsSaved()
        {
            // Arrange
            this.RemoveTestFile();
            var stockList = this.GetSampleStockList();
            var repository = new JsonFileStockRepository(TestStockFileName);
            var referenceHash = this.GetFileHash(ReferenceStockFile);

            // Act
            var result = repository.SaveStockListAsync(stockList).Result;

            // Assert
            var testHash = this.GetFileHash(TestStockFileName);

            Assert.Equal(referenceHash, testHash);
        }

        /// <summary>
        /// Tests that a persisted stock list can be loaded.
        /// </summary>
        [Fact]
        public void GivenReferenceStockFile_WhenLoadStockList_ThenStockListIsLoaded()
        {
            // Arrange
            var repository = new JsonFileStockRepository(ReferenceStockFile);
            var sampleStock = this.GetSampleStockList();

            // Act
            var stockList = repository.LoadStockListAsync().Result;

            // Assert
            var jsonSetting = new JsonSerializerSettings { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto };
            var sampleJson = JsonConvert.SerializeObject(sampleStock, jsonSetting);
            var stockListJson = JsonConvert.SerializeObject(stockList, jsonSetting);

            Assert.Equal(sampleJson, stockListJson);
        }

        /// <summary>
        /// Gets the sample stock list.
        /// </summary>
        /// <returns>List of <see cref="StockItem"/>.</returns>
        private IList<StockItem> GetSampleStockList()
        {
            var stock = new List<StockItem>();

            stock.Add(this.GetBrie());
            stock.Add(this.GetDexterityVest());
            stock.Add(this.GetExilir());
            stock.Add(this.GetSulfuras());
            stock.Add(this.GetBackstagePass());
            stock.Add(this.GetConjuredCake());

            // Set the date when the items were added to stock
            foreach (var item in stock)
            {
                item.AddedToStockUtc = TestStockDate;
            }

            return stock;
        }

        /// <summary>
        /// Gets the brie.
        /// </summary>
        /// <returns>Item with quality rules</returns>
        private StockItem GetBrie()
        {
            var item = new StockItem { Name = "Aged Brie", SellIn = 2, Quality = 0 };

            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { QualityAdjustment = 1 });
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { ActiveFromSellIn = -1,  QualityAdjustment = 1 });

            return item;
        }

        /// <summary>
        /// Gets the dexterity vest.
        /// </summary>
        /// <returns>Item with quality rules</returns>
        private StockItem GetDexterityVest()
        {
            var item = new StockItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };

            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { QualityAdjustment = -1 });
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { ActiveFromSellIn = -1, QualityAdjustment = -1 });

            return item;
        }

        /// <summary>
        /// Gets the exilir.
        /// </summary>
        /// <returns>Item with quality rules</returns>
        private StockItem GetExilir()
        {
            var item = new StockItem { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 };

            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { QualityAdjustment = -1 });
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { ActiveFromSellIn = -1, QualityAdjustment = -1 });

            return item;
        }

        /// <summary>
        /// Gets the sulfuras.
        /// </summary>
        /// <returns>Item with quality rules</returns>
        private StockItem GetSulfuras()
        {
            var item = new StockItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
            item.SellInRule.DailyAdjustment = 0;

            return item;
        }

        /// <summary>
        /// Gets the backstage pass.
        /// </summary>
        /// <returns>Item with quality rules</returns>
        private StockItem GetBackstagePass()
        {
            var item = new StockItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 };

            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { ActiveUntilSellIn = 0, QualityAdjustment = 1 });
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { ActiveFromSellIn = 10, ActiveUntilSellIn = 0, QualityAdjustment = 1 });
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { ActiveFromSellIn = 5,  ActiveUntilSellIn = 0, QualityAdjustment = 1 });
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityAbsolute { ActiveFromSellIn = -1, ActiveUntilSellIn = -1, QualityValue = 0 });

            return item;
        }

        /// <summary>
        /// Gets the conjured cake.
        /// </summary>
        /// <returns>Item with quality rules</returns>
        private StockItem GetConjuredCake()
        {
            var item = new StockItem { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 };
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { QualityAdjustment = -2 });
            item.QualityAdjustmentRules.Add(new QualityUpdateRuleQualityDelta { ActiveFromSellIn = -1, QualityAdjustment = -2 });

            return item;
        }

        /// <summary>
        /// Removes the test file if it exists.
        /// </summary>
        private void RemoveTestFile()
        {
            if (File.Exists(TestStockFileName))
            {
                File.Delete(TestStockFileName);
            }
        }

        /// <summary>
        /// Calculates the hash of the file specified.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>Hex representation of file hash.</returns>
        private string GetFileHash(string filename)
        {
            string hashHex;

            using (var hashAlgorithm = MD5.Create())
            {
                var filedata = File.ReadAllBytes(filename);

                hashAlgorithm.ComputeHash(filedata);

                hashHex = BitConverter.ToString(hashAlgorithm.Hash).Replace("-", string.Empty);
            }

            return hashHex;
        }
    }
}
