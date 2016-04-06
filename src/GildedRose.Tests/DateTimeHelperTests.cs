namespace GildedRose.Tests
{
    using System;

    using GildedRose.Logic;

    using Xunit;

    /// <summary>
    /// Defines the tests for the DateTimeHelper.
    /// </summary>
    public class DateTimeHelperTests
    {
        /// <summary>
        /// Gets the data of the moon landing.
        /// </summary>
        /// <value>
        /// The moon landing date.
        /// </value>
        private static DateTime MoonLanding => new DateTime(1969, 7, 20);

        /// <summary>
        /// Gets the date of the ZX Spectrum lauch.
        /// </summary>
        /// <value>
        /// The zx spectrum lauch date.
        /// </value>
        private static DateTime ZxSpectrumLauch => new DateTime(1982, 4, 21);

        /// <summary>
        /// Asserts that DateTimeHelper correctly returns the most recent of two dates.
        /// </summary>
        [Fact]
        public void GivenTwoDatesInChronologicalOrder_WhenMostRecent_ThenSecondDateIsReturned()
        {
            // Act
            var mostRecent = DateTimeHelper.MostRecent(MoonLanding, ZxSpectrumLauch);

            // Assert
            Assert.Equal(ZxSpectrumLauch, mostRecent);
        }

        /// <summary>
        /// Asserts that DateTimeHelper correctly returns the most recent of two dates.
        /// </summary>
        [Fact]
        public void GivenTwoDatesInReverseChronologicalOrder_WhenMostRecent_ThenFirstDateIsReturned()
        {
            // Act
            var mostRecent = DateTimeHelper.MostRecent(ZxSpectrumLauch, MoonLanding);

            // Assert
            Assert.Equal(ZxSpectrumLauch, mostRecent);
        }
    }
}
