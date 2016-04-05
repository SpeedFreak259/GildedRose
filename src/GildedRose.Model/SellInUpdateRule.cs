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
        /// Defaults DailyAdjustment to 1.
        /// </para>
        /// </summary>
        public SellInUpdateRule()
        {
            this.DailyAdjustment = 1;
        }

        /// <summary>
        /// Gets or sets the daily adjustment.
        /// </summary>
        /// <value>
        /// The daily adjustment.
        /// </value>
        public int DailyAdjustment { get; protected set;  }
    }
}