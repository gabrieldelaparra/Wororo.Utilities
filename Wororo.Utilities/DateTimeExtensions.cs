using System;
using System.Globalization;

namespace Wororo.Utilities
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static int GetCalendarWeek(this DateTime date)
        {
            return CultureInfo.CurrentCulture.Calendar
                              .GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}
