using System.Globalization;

namespace STS.SharedKernel.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? NationalIdBirthDateToDateTime(this string nationalId)
        {
            Dictionary<int, string> Century = new Dictionary<int, string>()
            {
                { 1, "18" },
                { 2, "19" },
                { 3, "20" },
                { 4, "21" }
            };
            if (string.IsNullOrEmpty(nationalId))
                return null;

            var centuryDateString = nationalId.Substring(0, 7);
            if (!int.TryParse(centuryDateString, out int parsed))
                return null;

            int centuryId = int.Parse(centuryDateString.Substring(0, 1));
            bool isValidCentury = Century.TryGetValue(centuryId, out string centuryValue);
            if (!isValidCentury)
                return null;
            var yearPortion = centuryValue + centuryDateString.Substring(1, 2);
            var monthPortion = centuryDateString.Substring(3, 2);
            var dayPortion = centuryDateString.Substring(5, 2);
            var date = $"{yearPortion}-{monthPortion}-{dayPortion}";

            bool isValidDate = DateTime.TryParse(date, out DateTime DOB);
            if (!isValidDate)
                return null;
            return DOB;
        }


        /// <summary>
        /// Returns the first day of the week that the specified date is in
        /// using the current culture.
        /// </summary>
        /// <param name="dayInWeek"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = dayInWeek.Date;

            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, string startDay)
        {
            DayOfWeek firstDay = ParseEnum<DayOfWeek>(startDay);
            return GetFirstDayOfWeek(dayInWeek, firstDay);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            return GetFirstDayOfWeek(dayInWeek, firstDay);
        }

        public static DateTime GetLastDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetLastDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        public static DateTime GetLastDayOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = GetFirstDayOfWeek(dayInWeek, firstDay);
            return firstDayInWeek.AddDays(6);
        }

        public static DateTime GetLastDayOfWeek(DateTime dayInWeek, string startDay)
        {
            DateTime firstDayInWeek = GetFirstDayOfWeek(dayInWeek, startDay);
            return firstDayInWeek.AddDays(7);
        }

        public static DateTime GetLastDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DateTime firstDayInWeek = GetFirstDayOfWeek(dayInWeek, cultureInfo);
            return firstDayInWeek.AddDays(7);
        }


        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
