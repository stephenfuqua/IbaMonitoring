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


        public static bool IsEmptyGuid(this Guid input)
        {
            return input == Guid.Empty;
        }

        public static bool IsEmptyGuid(this string input)
        {
            return input == Guid.Empty.ToString();
        }

        public static Guid ToGuid(this string input)
        {
            Guid output;
            if (Guid.TryParse(input, out output))
            {
                return output;
            }
            else
            {
                throw new ArgumentException("input is not a valid Guid", "input");
            }
        }

        public static int ToInt(this string input)
        {
            int output;
            if (int.TryParse(input, out output))
            {
                return output;
            }
            else
            {
                throw new ArgumentException("input is not a valid Guid", "input");
            }
        }

        public static Byte ToByte(this string input)
        {
            Byte output;
            if (Byte.TryParse(input, out output))
            {
                return output;
            }
            else
            {
                throw new ArgumentException("input is not a valid Guid", "input");
            }
        }

        public static DateTime ToDateTime(this string date, string time = "")
        {
            DateTime output;
            if (DateTime.TryParse(date + " " + time, out output))
            {
                return output;
            }
            else
            {
                throw new ArgumentException("input is not a valid DateTime", "date");
            }
        }
    }
}
