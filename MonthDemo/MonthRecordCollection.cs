using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class MonthRecordCollection
    {
        private List<MonthRecord> mRecords = new List<MonthRecord>();
        public List<MonthRecord> Records
        {
            get
            {
                return mRecords;
            }
        }

        private int mYear;
        public int Year
        {
            get
            {
                return mYear;
            }
        }

        private int mMonth;
        public int Month
        {
            get
            {
                return mMonth;
            }
        }

        private int mExpectedWorkHours;
        public int ExpectedWorkHours
        {
            get
            {
                return mExpectedWorkHours;
            }
        }

        public static MonthRecordCollection LoadFile(String path)
        {
            var result = new MonthRecordCollection();
            var records = AttendanceDate.LoadFile(path);
            foreach (var item in records.Values)
            {
                result.mRecords.Add(new MonthRecord(item));
            }
            result.mYear = result.mRecords[0].Year;
            result.mMonth = result.mRecords[0].Month;
            return result;
        }

        public void setNotWorkdays(List<DateTime> notWorkdays) 
        {
            int workDaysCount = DateTime.DaysInMonth(mYear, mMonth) - notWorkdays.Count;
            mExpectedWorkHours = workDaysCount * 8;
            foreach (var item in mRecords)
            {
                item.setNotWorkdays(notWorkdays);
            }
        }

        public void assemb()
        {
            foreach (var item in mRecords)
            {
                item.assemb();
            }
            mRecords.Sort((left, right) => right.Minutes.CompareTo(left.Minutes));
        }
    }
}
