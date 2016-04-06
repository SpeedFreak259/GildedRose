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

            var brie = new StockItem
            {
                Name = "Brie",
                Quality = 5,
                AddedToStockUtc = new DateTime(2016, 1, 1),
                SellIn = 8
            };

            var brieAdjustmentRule = new QualityUpdateRuleQualityDelta { QualityAdjustment = 1 };

            brie.QualityAdjustmentRules.Add(brieAdjustmentRule);

            var bread = new StockItem
            {
                Name = "Bread",
                Quality = 10,
                AddedToStockUtc = new DateTime(2016, 1, 1),
                SellIn = 7
            };

            var breadAdjustmentRule1 = new QualityUpdateRuleQualityDelta { ActiveUntilSellIn = 0, QualityAdjustment = -1 };
            var breadAdjustmentRule2 = new QualityUpdateRuleQualityDelta { ActiveFromSellIn = -1, QualityAdjustment = -2 };

            bread.QualityAdjustmentRules.Add(breadAdjustmentRule1);
            bread.QualityAdjustmentRules.Add(breadAdjustmentRule2);

            stock.Add(brie);
            stock.Add(bread);

            return stock;
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
