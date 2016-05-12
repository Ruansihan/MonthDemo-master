using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MonthDemo
{
    public class MyCalendar: Calendar
    {
        private List<DateTime> mNotWorkdays = new List<DateTime>();
        public List<DateTime> NotWorkdays
        {
            get
            {
                return mNotWorkdays;
            }
        }

        public void init(int year, int month)
        {
            var start = new DateTime(year, month, 1);
            var end = getLastDayInTheSameMonth(start);
            initNotWorkdays(start, end);
            this.DisplayDateStart = start;
            this.DisplayDateEnd = end;
        }

        private void initNotWorkdays(DateTime start, DateTime end)
        {
            DateTime date = start;
            while (date <= end)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday)
                {
                    this.NotWorkdays.Add(date);
                }
                date = date.AddDays(1);
            }
        }

        private static DateTime getLastDayInTheSameMonth(DateTime date)
        {
            var temp = date;
            if ((temp = date.AddDays(30)).Month == date.Month)
            {
                return temp;
            }
            else if((temp = date.AddDays(29)).Month == date.Month)
            {
                return temp;
            }
            else if ((temp = date.AddDays(28)).Month == date.Month)
            {
                return temp;
            }
            else
            {
                return date.AddDays(27);
            }
        }
    }
}
