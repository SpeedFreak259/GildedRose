// <copyright file="SellInUpdateRule.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>
namespace GildedRose.Model
{
    /// <summary>
    /// Defines the Sell In update rule, defaults to reduction in 1 SellIn per day.
    /// </summary>
    public class SellInUpdateRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellInUpdateRule"/> class.
        /// <para>
        /// Defaults DailyAdjustment to 1, covering full SellIn range.
        /// </para>
        /// </summary>
        public SellInUpdateRule()
        {
            this.DailyAdjustment = 1;
            this.ActiveFromSellIn = int.MaxValue;
            this.ActiveUntilSellIn = int.MinValue;
        }

        /// <summary>
        /// Gets or sets the daily adjustment.
        /// </summary>
        /// <value>
        /// The daily adjustment.
        /// </value>
        public int DailyAdjustment { get; protected set;  }

        /// <summary>
        /// Gets or sets the remaining SellIn days when this rule becomes active.
        /// </summary>
        /// <value>
        /// The number of days from SellIn when this rule becomes active.
        /// </value>
        public int ActiveFromSellIn { get; set; }

        /// <summary>
        /// Gets or sets the remaining SellIn days when this rule becomes inactive
        /// </summary>
        /// <value>
        /// The number of days from SellIn when this rule becomes inactive.
        /// </value>
        public int ActiveUntilSellIn { get; set; }
    }
}