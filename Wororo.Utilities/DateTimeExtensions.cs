using System;
using System.Globalization;

namespace Wororo.Utilities
{
    /// <summary>
    ///     Extension methods for DateTime class
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Returns the start of the week for a given date
        /// </summary>
        /// <param name="date">The date to calculate the start of the week from</param>
        /// <param name="startOfWeek">The first day of the week, Monday by default</param>
        /// <returns>The start of the week for a given date</returns>
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        ///     Gets the calendar week number for a given date
        /// </summary>
        /// <param name="date">The date to get the calendar week number from</param>
        /// <returns>The calendar week number for a given date</returns>
        public static int GetCalendarWeek(this DateTime date)
        {
            return CultureInfo.CurrentCulture.Calendar
                              .GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}
