// <copyright file="JsonFileStockRepository.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using GildedRose.Model;
    using Newtonsoft.Json;

    using Properties;

    /// <summary>
    /// Defines a simple implementation of <see cref="IStockRepository"/> that records to a local json formatted text file.
    /// </summary>
    /// <seealso cref="GildedRose.Logic.IStockRepository" />
    public class JsonFileStockRepository : IStockRepository
    {
        private const int FileBufferSizeBytes = 8192;

        /// <summary>
        /// The stock filename.
        /// </summary>
        private string stockFilename;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFileStockRepository"/> class.
        /// </summary>
        /// <param name="stockFileName">The stock file filename.</param>
        public JsonFileStockRepository(string stockFileName)
        {
            this.stockFilename = stockFileName;
        }

        /// <summary>
        /// Loads the stock list.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="IList{T}" /> of <see cref="StockItem" />.
        /// </returns>
        public async Task<IList<StockItem>> LoadStockListAsync()
        {
            try
            {
                // load the file
                var json = await this.ReadFileAsync();

                // deserialise
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                var stockList = JsonConvert.DeserializeObject<List<StockItem>>(json, settings);

                return stockList;
            }
            catch (FileNotFoundException)
            {
                Trace.TraceError(TraceMessages.FileNotFound, this.stockFilename);
                throw;
            }
            catch (Exception ex)
            {
                Trace.TraceError(TraceMessages.UnableToLoadStock, ex);
                throw;
            }
        }

        /// <summary>
        /// Saves the stock list.
        /// </summary>
        /// <param name="stockList">The stock items to persist.</param>
        /// <returns>
        /// A task of bool.
        /// </returns>
        public async Task<bool> SaveStockListAsync(IEnumerable<StockItem> stockList)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(stockList, settings);

                await this.WriteFileAsync(json);

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(TraceMessages.FailedToSaveStock, ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Reads the stock file using async I/O.
        /// </summary>
        /// <returns>The file content as string.</returns>
        private async Task<string> ReadFileAsync()
        {
            var content = new StringBuilder();

            using (FileStream fileStream = new FileStream(this.stockFilename, FileMode.Open, FileAccess.Read, FileShare.Read, FileBufferSizeBytes, useAsync: true))
            {
                byte[] buffer = new byte[FileBufferSizeBytes];
                int bytesRead = 0;

                while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length - 1)) != 0)
                {
                    content.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                }
            }

            return content.ToString();
        }

        /// <summary>
        /// Writes the file using async I/O.
        /// </summary>
        /// <param name="jsonData">The json string.</param>
        /// <returns>A Task.</returns>
        private async Task WriteFileAsync(string jsonData)
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

            using (var fileStream = new FileStream(this.stockFilename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, FileBufferSizeBytes, useAsync: true))
            {
                await fileStream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
            }
        }
    }
}
