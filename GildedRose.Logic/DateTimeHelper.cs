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
        /// <param name="firstDateTime">The datetime1.</param>
        /// <param name="secondDateTime">The datetime2.</param>
        /// <returns>Most recent of two date times.</returns>
        public static DateTime MostRecent(DateTime firstDateTime, DateTime secondDateTime)
        {
            if (firstDateTime > secondDateTime)
            {
                return firstDateTime;
            }

            return secondDateTime;
        }
    }
}
