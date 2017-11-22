using System;
using System.Globalization;

namespace _02.Date_Modifier
{
    public class DateModifier
    {
        private string startDate;
        private string endDate;

        public DateModifier(string startDate, string endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public int DifferenceBwtweenTwoDates()
        {
            DateTime start = DateTime.ParseExact(this.startDate, "yyyy M d", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(this.endDate, "yyyy M d", CultureInfo.InvariantCulture);

            return Math.Abs((int)(start - end).TotalDays);
        }
    }
}
