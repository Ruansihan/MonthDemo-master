using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonthDemo
{
    public class Record
    {
        public enum RecordType
        {
            WORKDAY,
            WORKDAY_ERROR,
            NOT_WORKDAY,
            NOT_WORKDAY_ERROR,
            ABSENTEEISM,
            REALLY_OVERTIME,
            REALLY_SHORT
        }
        const int REALLY_OVERTIME = 10 * 60;
        const int REALLY_SHORT = 6 * 60;
        public static Record create(List<DateTime> records, Predicate<DateTime> isWorkday)
        {
            int minutes = 0;
            RecordType type = RecordType.WORKDAY;
            var day = new DateTime(records[0].Year, records[0].Month, records[0].Day);
            if (records.Count >= 2)
            {
                var start = records[0];
                var end = records[records.Count - 1];
                var startHour = start.Hour;
                var endHour = end.Hour >= startHour ? end.Hour : end.Hour + 24;
                //过了下午两点的工作时要扣取午休时间
                endHour = endHour > 14 ? endHour - 1 : endHour;
                minutes = (endHour - startHour) * 60 + (end.Minute - start.Minute);
            }
            if (isWorkday(records[0]))
            {
                if (records.Count >= 2)
                {
                    if (minutes > REALLY_OVERTIME)
                    {
                        type = RecordType.REALLY_OVERTIME;
                    }
                    else if (minutes < REALLY_SHORT)
                    {
                        type = RecordType.REALLY_SHORT;
                    }
                    else
                    {
                        type = RecordType.WORKDAY;
                    }
                }
                else
                {
                    type = RecordType.WORKDAY_ERROR;
                }
            }
            else
            {
                if (records.Count >= 2)
                {
                    type = RecordType.NOT_WORKDAY;
                }
                else
                {
                    type = RecordType.NOT_WORKDAY_ERROR;
                }
            }
            return new Record(records, minutes, day, type);
        }

        public static Record createAbsenteeismRecord(DateTime day)
        {
            return new Record(null, 0, day, RecordType.ABSENTEEISM);
        }

        private Record(List<DateTime> records, int minutes, DateTime day, RecordType type)
        {
            mRecordsOfDay = records;
            mMinutes = minutes;
            mDay = day;
            mType = type;
        }
        protected int mMinutes;
        public int Minutes
        {
            get
            {
                return mMinutes;
            }
        }
        protected RecordType mType;
        public RecordType Type
        {
            get
            {
                return mType;
            }
        }
        protected DateTime mDay;
        public DateTime Day
        {
            get
            {
                return mDay;
            }
        }
        protected List<DateTime> mRecordsOfDay;
        public List<DateTime> RecordsOfDay
        {
            get
            {
                return mRecordsOfDay;
            }
        }
    }
}
