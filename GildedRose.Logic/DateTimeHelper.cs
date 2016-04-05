// <copyright file="DateTimeHelper.cs" company="Andy Baker">
// See MIT-LICENSE.txt
// </copyright>

namespace GildedRose.Logic
{
    using System;

    /// <summary>
    /// Defines static helper functions for date time object.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Returns the most recent of two date times.
        /// </summary>
        /// <param name="datetime1">The datetime1.</param>
        /// <param name="datetime2">The datetime2.</param>
        /// <returns>Most recent of two date times.</returns>
        public static DateTime MostRecent(DateTime datetime1, DateTime datetime2)
        {
            if (datetime1 > datetime2)
            {
                return datetime1;
            }

            return datetime2;
        }
    }
}
