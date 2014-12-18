using System;

namespace safnet.iba.Static
{
    /// <summary>
    /// Common functions for data conversions.
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Gets the date for week number.
        /// </summary>
        /// <param name="weekNumber">The week number.</param>
        /// <returns>Date as string (month/day)</returns>
        public static string GetDateForWeekNumber(int weekNumber)
        {
            DateTime date = new DateTime(2010, 01, 01);
            date = date.AddDays(7 * weekNumber);
            while (date.DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(-1);
            }

            // This gives us a date that is one week too far in the future
            date = date.AddDays(-7);

            return date.ToString("M/d");
        }
    }
}
